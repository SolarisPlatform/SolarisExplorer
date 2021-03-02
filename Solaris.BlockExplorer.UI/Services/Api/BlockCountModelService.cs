using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class BlockCountModelService : IBlockCountModelService
    {
        private readonly IBlockCountService _blockCountService;

        public BlockCountModelService(IBlockCountService blockCountService)
        {
            _blockCountService = blockCountService;
        }

        public Task<long> GetBlockCount()
        {
            return _blockCountService.GetBlockCount();
        }
    }
}
