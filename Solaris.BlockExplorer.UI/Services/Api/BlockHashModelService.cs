using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class BlockHashModelService : IBlockHashModelService
    {
        private readonly IBlockHashService _blockHashService;

        public BlockHashModelService(IBlockHashService blockHashService)
        {
            _blockHashService = blockHashService;
        }

        public Task<string> GetBlockHash(long index)
        {
            return _blockHashService.GetBlockHash(index);
        }
    }
}
