using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class TransactionInputRepository : ITransactionInputRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionInputRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<Entities.Read.TransactionInput>> GetTransactionInputs(string transactionId)
        {
            return _dbConnection.QueryAsync<Entities.Read.TransactionInput>("storedprocedures.GetTransactionInputs",
                new {transactionId}, commandType: CommandType.StoredProcedure);
        }
    }
}
