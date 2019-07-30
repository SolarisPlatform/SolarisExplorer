using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Entities.Read.Transaction> GetTransaction(string transactionId)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<Entities.Read.Transaction>("storedprocedures.GetTransaction", new
            {
                transactionId
            }, commandType: CommandType.StoredProcedure);
        }

        public Task Insert(Entities.Create.Transaction transaction)
        {
            return _dbConnection.ExecuteAsync("storedprocedures.InsertTransaction", transaction,
                commandType: CommandType.StoredProcedure);
        }
    }
}
