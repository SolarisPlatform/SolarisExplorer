using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class TransactionCountDataRepository : ITransactionCountDataRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionCountDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<TransactionCountData>> GetTransactionCount()
        {
            return _dbConnection.QueryAsync<TransactionCountData>("storedprocedures.GetTransactionCountData", commandType: CommandType.StoredProcedure);
        }
    }
}
