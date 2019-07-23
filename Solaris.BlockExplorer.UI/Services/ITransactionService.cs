using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<ITransactionModel>> GetTransactions(string blockHash);
        Task<ITransactionModel> GetTransaction(string transactionHash);
    }
}