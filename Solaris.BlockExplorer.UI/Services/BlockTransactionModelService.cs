using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockTransactionModelService : IBlockTransactionModelService
    {
        private readonly IBlockTransactionService _blockTransactionService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public BlockTransactionModelService(IBlockTransactionService blockTransactionService, IMapper mapper, IMemoryCache memoryCache)
        {
            _blockTransactionService = blockTransactionService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<IBlockTransactionModel>> GetBlockTransactions(string blockId)
        {
            var key = $"BlockTransactions_{blockId}";

            if (!_memoryCache.TryGetValue(key, out IEnumerable<BlockTransactionModel> blockTransactions))
            {
                blockTransactions = _mapper.Map<IEnumerable<BlockTransactionModel>>(await _blockTransactionService.GetBlockBlockTransactions(blockId));
                CacheItemKeys.Keys.Add(key);
                _memoryCache.Set(key, blockTransactions);
            }


            return blockTransactions;
        }
    }
}
