using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockSizeDataModelService : IBlockSizeDataModelService
    {
        private readonly IBlockSizeDataService _blockSizeDataService;
        private readonly IMapper _mapper;

        public BlockSizeDataModelService(IBlockSizeDataService blockSizeDataService, IMapper mapper)
        {
            _blockSizeDataService = blockSizeDataService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlockSizeDataModel>> GetBlockSizeData()
        {
            var blockSizeData = await _blockSizeDataService.GetBlockSizeData();
            return _mapper.Map<IEnumerable<BlockSizeDataModel>>(blockSizeData);
        }
    }
}
