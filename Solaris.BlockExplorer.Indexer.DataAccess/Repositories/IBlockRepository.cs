using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.DataAccess.Models;

namespace Solaris.BlockExplorer.Indexer.DataAccess.Repositories
{
    public interface IBlockRepository
    {
        Task Insert(Block block);
        Task<long> GetCurrent();
    }
}