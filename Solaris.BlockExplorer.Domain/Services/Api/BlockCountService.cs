using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class BlockCountService : IBlockCountService
    {
        private readonly IBlockCountRepository _blockCountRepository;

        public BlockCountService(IBlockCountRepository blockCountRepository)
        {
            _blockCountRepository = blockCountRepository;
        }

        public Task<long> GetBlockCount()
        {
            return _blockCountRepository.GetBlockCount();
        }
    }
}
