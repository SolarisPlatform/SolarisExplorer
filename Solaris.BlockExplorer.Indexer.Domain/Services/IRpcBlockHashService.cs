using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public interface IRpcBlockHashService
    {
        Task<string> GetBlockHash(long height);
    }
}