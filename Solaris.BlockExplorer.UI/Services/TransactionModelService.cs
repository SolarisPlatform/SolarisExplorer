using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionModelService : ITransactionModelService
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public TransactionModelService(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<ITransactionModel> GetTransaction(string transactionId)
        {
            var transaction = await _transactionService.GetTransaction(transactionId);

            return _mapper.Map<TransactionModel>(transaction);
        }
    }
}
