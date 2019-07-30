using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public interface IRpcBlockService
    {
        Task<IRpcBlock> GetBlock(long height);
    }
}