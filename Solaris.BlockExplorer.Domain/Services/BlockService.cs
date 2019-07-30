using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IMapper _mapper;
        public BlockService(IBlockRepository blockRepository, IMapper mapper)
        {
            _blockRepository = blockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Block>> GetBlocks()
        {
            var blocks = await _blockRepository.GetBlocks();
            return _mapper.Map<IEnumerable<Block>>(blocks);
        }

        public async Task<Block> GetBlock(string blockId)
        {
            var block = await _blockRepository.GetBlock(blockId);
            return _mapper.Map<Block>(block);
        }

        public Task<long> GetBlockHeight()
        {
            return _blockRepository.GetBlockHeight();
        }
    }
}
