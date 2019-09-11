using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IBlockRepository
    {
        Task<Entities.Read.PagedResult<IEnumerable<Entities.Read.Block>>> GetBlocks(Entities.Read.Paging paging);
        Task<Entities.Read.Block> GetBlock(string blockId);
        Task<long> GetBlockHeight();
        Task Insert(Entities.Create.Block block);
    }
}