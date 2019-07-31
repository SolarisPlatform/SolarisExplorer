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
    }
}
