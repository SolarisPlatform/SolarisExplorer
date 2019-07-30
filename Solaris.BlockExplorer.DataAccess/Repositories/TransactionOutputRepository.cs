using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;


namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class TransactionOutputRepository : ITransactionOutputRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionOutputRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<Entities.Read.TransactionOutput>> GetTransactionOutputs(string transactionId)
        {
            return _dbConnection.QueryAsync<Entities.Read.TransactionOutput>("storedprocedures.GetTransactionOutputs", new {transactionId}, commandType: CommandType.StoredProcedure);
        }

        public Task<Guid> Insert(Entities.Create.Output output)
        {
            return _dbConnection.QuerySingleAsync<Guid>("storedprocedures.InsertOutput", output,
                commandType: CommandType.StoredProcedure);
        }
    }
}
