using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockService : IBlockService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IConfiguration _configuration;
        public BlockService(IMongoDatabase mongoDatabase, IConfiguration configuration)
        {
            _mongoDatabase = mongoDatabase;
            _configuration = configuration;
        }

        public async Task<IEnumerable<IBlockModel>> GetBlocks(int limit = 150)
        {
            var collection = GetCollection();

            var findOptions = new FindOptions<BlockModel, BlockModel>
            {
                Limit = limit,
                Sort = Builders<BlockModel>.Sort.Ascending(block => block.Id)
            };
            var findResults = await collection.FindAsync(FilterDefinition<BlockModel>.Empty, findOptions);

            return findResults.ToList();
        }

        public async Task<IBlockModel> GetBlock(string blockHash)
        {
            var collection = GetCollection();
            var result = await collection.FindAsync(p => p.Hash.Equals(blockHash));

            return result.FirstOrDefault();
        }

        public async Task<IBlockModel> GetLastBlock()
        {
            var collection = GetCollection();
            var findOptions = new FindOptions<BlockModel, BlockModel>
            {
                Limit = 1,
                Sort = Builders<BlockModel>.Sort.Descending(block => block.Id)
            };
            var findResults = await collection.FindAsync(FilterDefinition<BlockModel>.Empty, findOptions);

            var current = findResults.FirstOrDefault();

            return current;
        }

        private IMongoCollection<BlockModel> GetCollection()
        {
            var section = _configuration.GetSection("MongoDB");
            var blockCollectionName = section.GetValue<string>("BlockCollectionName");

            return _mongoDatabase.GetCollection<BlockModel>(blockCollectionName);
        }
    }
}
