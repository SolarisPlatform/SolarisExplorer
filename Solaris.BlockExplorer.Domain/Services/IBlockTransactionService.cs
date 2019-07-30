using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface IBlockTransactionService
    {
        Task<IEnumerable<BlockTransaction>> GetBlockBlockTransactions(string blockId);
    }
}