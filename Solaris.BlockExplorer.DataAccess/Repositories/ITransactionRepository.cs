using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface ITransactionRepository
    {
        Task<Entities.Read.Transaction> GetTransaction(string transactionId);
        Task Insert(Entities.Create.Transaction transaction);
    }
}