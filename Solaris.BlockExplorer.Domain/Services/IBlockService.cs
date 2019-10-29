using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface IBlockService
    {
        Task<PagedResult<IEnumerable<Block>>> GetBlocks(Paging paging);
        Task<Block> GetBlock(string blockId);
        Task<Block> GetBlock(long height);
        Task<long> GetBlockHeight();
    }
}