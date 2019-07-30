using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<IEnumerable<IBlockModel>> GetBlocks()
        {
            var blocks = await _blockService.GetBlocks();
            return _mapper.Map<IEnumerable<BlockModel>>(blocks);
        }

        public async Task<IBlockModel> GetBlock(string blockId)
        {
            var block = await _blockService.GetBlock(blockId);
            return _mapper.Map<BlockModel>(block);
        }
    }
}
