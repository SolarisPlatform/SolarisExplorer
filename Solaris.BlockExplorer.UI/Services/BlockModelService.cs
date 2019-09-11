using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Models;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockModelService : IBlockModelService
    {
        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;

        public BlockModelService(IBlockService blockService, IMapper mapper)
        {
            _blockService = blockService;
            _mapper = mapper;
        }

        public async Task<PagedResultModel<IEnumerable<BlockModel>>> GetBlocks(PagingModel paging)
        {
            var blocks = await _blockService.GetBlocks(_mapper.Map<Paging>(paging));
            return _mapper.Map<PagedResultModel<IEnumerable<BlockModel>>>(blocks);
        }

        public async Task<IBlockModel> GetBlock(string blockId)
        {
            var block = await _blockService.GetBlock(blockId);
            return _mapper.Map<BlockModel>(block);
        }

        public Task<long> GetBlockHeight()
        {
            return _blockService.GetBlockHeight();
        }
    }
}
