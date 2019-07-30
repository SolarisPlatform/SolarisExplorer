using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ITransactionOutputModelService
    {
        Task<IEnumerable<ITransactionOutputModel>> GetTransactionOutputs(string transactionId);
    }
}