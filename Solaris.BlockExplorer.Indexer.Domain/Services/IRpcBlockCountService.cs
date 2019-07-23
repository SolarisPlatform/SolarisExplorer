using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public interface IRpcBlockCountService
    {
        Task<long> GetBlockCount();
    }
}