using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IBlockCountRepository
    {
        Task<long> GetBlockCount();
    }
}