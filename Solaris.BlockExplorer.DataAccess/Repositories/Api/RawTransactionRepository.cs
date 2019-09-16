using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class RawTransactionRepository : IRawTransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public RawTransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<string> GetRawTransaction(string txId)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<string>("apistoredprocedures.GetRawTransaction", new
            {
                TxId = txId
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
