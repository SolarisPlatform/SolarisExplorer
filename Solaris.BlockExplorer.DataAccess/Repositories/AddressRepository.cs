using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDbConnection _dbConnection;

        public AddressRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Address> GetAddress(string publicKey)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<Address>("storedprocedures.GetAddress", new
                {
                    publicKey
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<PagedResult<IEnumerable<AddressTransaction>>> GetAddressTransactions(string publicKey, Paging paging)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReturnValue", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@PageSize", value: paging.PageSize, dbType: DbType.Int64, direction: ParameterDirection.Input);
            parameters.Add("@PageNumber", value: paging.PageNumber, dbType: DbType.Int64, direction: ParameterDirection.Input);
            parameters.Add("@PublicKey", value: publicKey, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbConnection.QueryAsync<AddressTransaction>("storedprocedures.GetAddressTransactions", parameters,
                commandType: CommandType.StoredProcedure);
            
            return new PagedResult<IEnumerable<AddressTransaction>>
            {
                Result = await result,
                CurrentPage = paging.PageNumber,
                PageSize = paging.PageSize,
                PageCount = parameters.Get<long>("@ReturnValue")
            };
        }
    }
}
