using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IBlockTransactionRepository
    {
        Task<IEnumerable<BlockTransaction>> GetBlockTransactions(string blockId);
    }
}