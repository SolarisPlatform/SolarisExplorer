using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Solaris.BlockExplorer.Domain.Models;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockModelService : IBlockModelService
    {
        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public BlockModelService(IBlockService blockService, IMapper mapper, IMemoryCache memoryCache)
        {
            _blockService = blockService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<PagedResultModel<IEnumerable<BlockModel>>> GetBlocks(PagingModel paging)
        {
            var key = $"Blocks_{paging.PageNumber}_{paging.PageSize}";

            if (!_memoryCache.TryGetValue(key, out PagedResult<IEnumerable<Block>> blocks))
            {
                blocks = await _blockService.GetBlocks(_mapper.Map<Paging>(paging));
                CacheItemKeys.Keys.Add(key);
                _memoryCache.Set(key, blocks);
            }

            return _mapper.Map<PagedResultModel<IEnumerable<BlockModel>>>(blocks);
        }

        public async Task<IBlockModel> GetBlock(string blockId)
        {
            var key = blockId;

            if (!_memoryCache.TryGetValue(key, out IBlockModel block))
            {
                block = _mapper.Map<BlockModel>(await _blockService.GetBlock(blockId));
                CacheItemKeys.Keys.Add(key);
                _memoryCache.Set(key, block);
            }

            return block;
        }

        public async Task<IBlockModel> GetBlock(long height)
        {
            var key = height;
            if (!_memoryCache.TryGetValue(key, out IBlockModel block))
            {
                block = _mapper.Map<BlockModel>(await _blockService.GetBlock(height));
                CacheItemKeys.Keys.Add(key.ToString());
                _memoryCache.Set(key, block);
            }

            return block;
        }

        public async Task<long> GetBlockHeight()
        {
            const string key = "BlockHeight";
            if (!_memoryCache.TryGetValue(key, out long blockHeight))
            {
                blockHeight = await _blockService.GetBlockHeight();
                CacheItemKeys.Keys.Add(key);
                _memoryCache.Set(key, blockHeight);
            }

            return blockHeight;
        }
    }
}
