using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IBlockHashRepository
    {
        Task<string> GetBlockHash(long index);
    }
}