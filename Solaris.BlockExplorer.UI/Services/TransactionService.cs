using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IConfiguration _configuration;

        public TransactionService(IMongoDatabase mongoDatabase, IConfiguration configuration)
        {
            _mongoDatabase = mongoDatabase;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ITransactionModel>> GetTransactions(string blockHash)
        {
            var collection = GetCollection();
            var result = await collection.FindAsync(p => p.BlockHash.Equals(blockHash));

            var transactions = await result.ToListAsync();

            foreach (var transaction in transactions)
            {
                foreach (var input in transaction.Inputs)
                {
                    var sourceTransaction = await GetTransaction(input.TxId);

                    var sourceOutput = sourceTransaction?.Outputs.FirstOrDefault(p => p.Index.Equals(input.Vout));

                    if (sourceOutput == null)
                        continue;

                    input.From = sourceOutput.ScriptPubKey.Addresses;
                }
            }

            return transactions;
        }

        public async Task<ITransactionModel> GetTransaction(string transactionHash)
        {
            var collection = GetCollection();
            var result = await collection.FindAsync(p => p.TxId.Equals(transactionHash));

            return await result.FirstOrDefaultAsync();
        }

        private IMongoCollection<TransactionModel> GetCollection()
        {
            var section = _configuration.GetSection("MongoDB");
            var transactionCollectionName = section.GetValue<string>("TransactionCollectionName");

            return _mongoDatabase.GetCollection<TransactionModel>(transactionCollectionName);
        }
    }
}
