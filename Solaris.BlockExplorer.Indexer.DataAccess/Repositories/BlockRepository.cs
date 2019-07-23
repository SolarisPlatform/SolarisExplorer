using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Solaris.BlockExplorer.Indexer.DataAccess.Models;

namespace Solaris.BlockExplorer.Indexer.DataAccess.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _mongoDatabase;

        public BlockRepository(IConfiguration configuration, IMongoDatabase mongoDatabase)
        {
            _configuration = configuration;
            _mongoDatabase = mongoDatabase;
        }

        public Task Insert(Block block)
        {
            var collection = GetCollection();
            return collection.InsertOneAsync(block);
        }

        public async Task<long> GetCurrent()
        {
            var collection = GetCollection();
            var findOptions = new FindOptions<Block, Block>
            {
                Limit = 1,
                Sort = Builders<Block>.Sort.Descending(block => block.Id)
            };
            var findResults = await collection.FindAsync(FilterDefinition<Block>.Empty, findOptions);

            var current = findResults.FirstOrDefault();

            return current?.Id ?? 0;
        }

        private IMongoCollection<Block> GetCollection()
        {
            var section = _configuration.GetSection("MongoDB");
            var blockCollectionName = section.GetValue<string>("BlockCollectionName");

            return _mongoDatabase.GetCollection<Block>(blockCollectionName);
        }
    }
}
