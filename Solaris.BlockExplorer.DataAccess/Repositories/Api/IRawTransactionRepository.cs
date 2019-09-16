using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IRawTransactionRepository
    {
        Task<string> GetRawTransaction(string txId);
    }
}