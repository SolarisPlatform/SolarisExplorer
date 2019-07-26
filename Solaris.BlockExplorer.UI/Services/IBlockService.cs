using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IBlockService
    {
        Task<Block[]> GetBlocks(int limit = 150);
        Task<Block> GetBlock(string blockHash);
        Task<Block> GetLastBlock();
    }
}