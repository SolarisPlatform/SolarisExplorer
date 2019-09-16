using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class RawTransactionModelService : IRawTransactionModelService
    {
        private readonly IRawTransactionService _rawTransactionService;

        public RawTransactionModelService(IRawTransactionService rawTransactionService)
        {
            _rawTransactionService = rawTransactionService;
        }

        public Task<string> GetRawTransaction(string txId)
        {
            return _rawTransactionService.GetRawTransaction(txId);
        }
    }
}
