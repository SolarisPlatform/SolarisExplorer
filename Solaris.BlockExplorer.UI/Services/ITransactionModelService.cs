using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ITransactionModelService
    {
        Task<ITransactionModel> GetTransaction(string transactionId);
    }
}