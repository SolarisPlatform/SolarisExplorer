using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ITransactionInputModelService
    {
        Task<IEnumerable<ITransactionInputModel>> GetTransactionInputs(string transactionId);
    }
}