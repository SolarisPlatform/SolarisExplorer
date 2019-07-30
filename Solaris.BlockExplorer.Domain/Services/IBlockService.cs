using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface IBlockService
    {
        Task<IEnumerable<Block>> GetBlocks();
        Task<Block> GetBlock(string blockId);
        Task<long> GetBlockHeight();
    }
}