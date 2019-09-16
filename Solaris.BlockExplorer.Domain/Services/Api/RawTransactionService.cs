using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class RawTransactionService : IRawTransactionService
    {
        private readonly IRawTransactionRepository _rawTransactionRepository;

        public RawTransactionService(IRawTransactionRepository rawTransactionRepository)
        {
            _rawTransactionRepository = rawTransactionRepository;
        }

        public Task<string> GetRawTransaction(string txId)
        {
            return _rawTransactionRepository.GetRawTransaction(txId);
        }
    }
}
