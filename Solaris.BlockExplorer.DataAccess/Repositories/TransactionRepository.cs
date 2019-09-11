using System.Collections.Generic;
using System.Data;
using System.Linq;
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


        public async Task Insert(Entities.Create.Transaction transaction, IEnumerable<Entities.Create.Input> inputs, IEnumerable<Entities.Create.Output> outputs)
        {
            var inputsTable = new DataTable();
            inputsTable.Columns.Add("Coinbase", typeof(string));
            inputsTable.Columns.Add("Sequence", typeof(long));
            inputsTable.Columns.Add("OutputTransactionId", typeof(string));
            inputsTable.Columns.Add("OutputIndex", typeof(long));
            inputsTable.Columns.Add("Asm", typeof(string));
            inputsTable.Columns.Add("Hex", typeof(string));

            foreach (var input in inputs)
            {
                inputsTable.Rows.Add(input.Coinbase, input.Sequence, input.OutputTransactionId, input.OutputIndex, input.InputScriptSignature?.Asm, input.InputScriptSignature?.Hex);
            }

            var outputsTable = new DataTable();
            outputsTable.Columns.Add("Value", typeof(decimal));
            outputsTable.Columns.Add("Index", typeof(long));
            outputsTable.Columns.Add("Asm", typeof(string));
            outputsTable.Columns.Add("Hex", typeof(string));
            outputsTable.Columns.Add("RequestedSignatures", typeof(long));
            outputsTable.Columns.Add("Type", typeof(string));
            outputsTable.Columns.Add("Addresses", typeof(string));

            foreach (var output in outputs)
            {
                var addresses = output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Select(p => p.Address);
                outputsTable.Rows.Add(output.Value, output.Index, output.OutputScriptPublicKey.Asm, output.OutputScriptPublicKey.Hex, output.OutputScriptPublicKey.RequestedSignatures, output.OutputScriptPublicKey.Type, string.Join(',', addresses));
            }

            await _dbConnection.ExecuteAsync("storedprocedures.InsertTransaction", new
            {
                BlockTime = transaction.BlockTime,
                Hash = transaction.Hash,
                Id = transaction.Id,
                Locktime = transaction.Locktime,
                Size = transaction.Size,
                Time = transaction.Time,
                Version = transaction.Version,
                Vsize = transaction.Vsize,
                BlockId = transaction.BlockId,
                Json = transaction.Json,
                BlockOrder = transaction.BlockOrder,
                Inputs = inputsTable,
                Outputs = outputsTable
            },
                commandType: CommandType.StoredProcedure);
        }
    }
}
