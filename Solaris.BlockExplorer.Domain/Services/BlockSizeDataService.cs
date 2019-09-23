using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class BlockSizeDataService : IBlockSizeDataService
    {
        private readonly IBlockSizeDataRepository _blockSizeDataRepository;
        private readonly IMapper _mapper;

        public BlockSizeDataService(IBlockSizeDataRepository blockSizeDataRepository, IMapper mapper)
        {
            _blockSizeDataRepository = blockSizeDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlockSizeData>> GetBlockSizeData()
        {
            var blockSizeData = await _blockSizeDataRepository.GetBlockSizeData();
            return _mapper.Map<IEnumerable<BlockSizeData>>(blockSizeData);
        }
    }
}
