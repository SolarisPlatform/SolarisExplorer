using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class BlockModelService : IBlockModelService
    {
        private readonly IBlockService _blockService;

        public BlockModelService(IBlockService blockService)
        {
            _blockService = blockService;
        }

        public Task<string> GetBlock(string hash)
        {
            return _blockService.GetBlock(hash);
        }
    }
}
