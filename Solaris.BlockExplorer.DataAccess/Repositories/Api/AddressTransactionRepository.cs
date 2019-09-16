using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read.Api;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class AddressTransactionRepository : IAddressTransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public AddressTransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<AddressTransaction>> GetAddressTransactions(string publicKey, long count, long min)
        {
            return _dbConnection.QueryAsync<AddressTransaction>("apistoredprocedures.GetAddressTransactions", new
            {
                PublicKey = publicKey,
                PageSize = count,
                PageNumber = min
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
