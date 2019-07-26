using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ITransactionService
    {
        Task<Transaction[]> GetTransactions(string blockHash);
        Task<Transaction[]> GetTransactionsForAddress(string address);
        Task<Transaction> GetTransaction(string transactionHash);

    }
}