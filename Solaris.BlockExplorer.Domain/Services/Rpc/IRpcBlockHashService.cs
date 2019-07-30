using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public interface IRpcBlockHashService
    {
        Task<string> GetBlockHash(long height);
    }
}