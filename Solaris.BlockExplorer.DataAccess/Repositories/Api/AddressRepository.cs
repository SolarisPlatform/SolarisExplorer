using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read.Api;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IAddressTransactionRepository _addressTransactionRepository;
        public AddressRepository(IDbConnection dbConnection, IAddressTransactionRepository addressTransactionRepository)
        {
            _dbConnection = dbConnection;
            _addressTransactionRepository = addressTransactionRepository;
        }

        public async Task<Address> GetAddress(string publicKey)
        {
            var address = await _dbConnection.QueryFirstOrDefaultAsync<Address>("apistoredprocedures.GetAddress", new
            {
                PublicKey = publicKey
            }, commandType: CommandType.StoredProcedure);

            var addressTransactions = await _addressTransactionRepository.GetAddressTransactions(publicKey, 100, 1);

            address.Transactions = addressTransactions.ToArray();

            return address;
        }
    }
}
