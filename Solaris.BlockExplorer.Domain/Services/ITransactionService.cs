using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransaction(string transactionId);
    }
}