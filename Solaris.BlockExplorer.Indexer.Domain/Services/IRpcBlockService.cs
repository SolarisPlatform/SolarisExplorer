using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.Domain.Models;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public interface IRpcBlockService
    {
        Task<IRpcBlock> GetBlock(long height);
    }
}