using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.DataAccess.Models;

namespace Solaris.BlockExplorer.Indexer.DataAccess.Repositories
{
    public interface ITransactionRepository
    {
        Task Insert(Transaction transaction);
    }
}