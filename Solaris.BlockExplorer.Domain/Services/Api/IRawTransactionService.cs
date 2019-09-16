using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IRawTransactionService
    {
        Task<string> GetRawTransaction(string txId);
    }
}