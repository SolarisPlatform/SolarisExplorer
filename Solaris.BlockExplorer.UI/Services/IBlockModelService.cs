using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IBlockModelService
    {
        Task<PagedResultModel<IEnumerable<BlockModel>>> GetBlocks(PagingModel paging);
        Task<IBlockModel> GetBlock(string blockId);
        Task<IBlockModel> GetBlock(long height);
        Task<long> GetBlockHeight();
    }
}