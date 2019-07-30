using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public interface IRpcTransactionService
    {
        Task<IRpcTransaction> GetTransaction(string txId);
    }
}