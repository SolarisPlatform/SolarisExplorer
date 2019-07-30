using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IBlockModelService
    {
        Task<IEnumerable<IBlockModel>> GetBlocks();
        Task<IBlockModel> GetBlock(string blockId);
        Task<long> GetBlockHeight();
    }
}