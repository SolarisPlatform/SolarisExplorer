using System.Collections.Generic;
using System.Threading.Tasks;


namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IBlockRepository
    {
        Task<IEnumerable<Entities.Read.Block>> GetBlocks();
        Task<Entities.Read.Block> GetBlock(string blockId);
        Task<long> GetBlockHeight();
        Task Insert(Entities.Create.Block block);
    }
}