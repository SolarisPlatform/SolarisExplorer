using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Solaris.BlockExplorer.Indexer.DataAccess.Models;

namespace Solaris.BlockExplorer.Indexer.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _mongoDatabase;

        public TransactionRepository(IConfiguration configuration, IMongoDatabase mongoDatabase)
        {
            _configuration = configuration;
            _mongoDatabase = mongoDatabase;
        }

        public Task Insert(Transaction transaction)
        {
            var collection = GetCollection();
            return collection.InsertOneAsync(transaction);
        }

        private IMongoCollection<Transaction> GetCollection()
        {
            var section = _configuration.GetSection("MongoDB");
            var transactionCollectionName = section.GetValue<string>("TransactionCollectionName");

            return _mongoDatabase.GetCollection<Transaction>(transactionCollectionName);
        }
    }
}
