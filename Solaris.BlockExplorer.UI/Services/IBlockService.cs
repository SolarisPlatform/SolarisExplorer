using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IBlockService
    {
        Task<IEnumerable<IBlockModel>> GetBlocks(int limit = 150);
        Task<IBlockModel> GetBlock(string blockHash);
        Task<IBlockModel> GetLastBlock();
    }
}