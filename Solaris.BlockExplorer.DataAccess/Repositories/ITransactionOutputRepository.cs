using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface ITransactionOutputRepository
    {
        Task<IEnumerable<Entities.Read.TransactionOutput>> GetTransactionOutputs(string transactionId);
        Task<Guid> Insert(Entities.Create.Output output);
    }
}