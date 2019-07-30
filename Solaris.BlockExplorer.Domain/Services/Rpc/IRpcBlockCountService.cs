using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public interface IRpcBlockCountService
    {
        Task<long> GetBlockCount();
    }
}