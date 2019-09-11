using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface ITransactionInputRepository
    {
        Task<IEnumerable<Entities.Read.TransactionInput>> GetTransactionInputs(string transactionId);
    }
}