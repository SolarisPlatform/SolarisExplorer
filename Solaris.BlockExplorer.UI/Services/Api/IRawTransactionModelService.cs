using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IRawTransactionModelService
    {
        Task<string> GetRawTransaction(string txId);
    }
}