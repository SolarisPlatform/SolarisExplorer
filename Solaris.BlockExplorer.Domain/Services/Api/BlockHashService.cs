using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class BlockHashService : IBlockHashService
    {
        private readonly IBlockHashRepository _blockHashRepository;

        public BlockHashService(IBlockHashRepository blockHashRepository)
        {
            _blockHashRepository = blockHashRepository;
        }

        public Task<string> GetBlockHash(long index)
        {
            return _blockHashRepository.GetBlockHash(index);
        }
    }
}
