using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IBlockTransactionModelService
    {
        Task<IEnumerable<IBlockTransactionModel>> GetBlockTransactions(string blockId);
    }
}