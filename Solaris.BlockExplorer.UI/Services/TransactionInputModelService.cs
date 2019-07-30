using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionInputModelService : ITransactionInputModelService
    {
        private readonly ITransactionInputService _transactionInputService;
        private readonly IMapper _mapper;

        public TransactionInputModelService(ITransactionInputService transactionInputService, IMapper mapper)
        {
            _transactionInputService = transactionInputService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ITransactionInputModel>> GetTransactionInputs(string transactionId)
        {
            var transactionInputs = await _transactionInputService.GetTransactionInputs(transactionId);

            return _mapper.Map<IEnumerable<TransactionInputModel>>(transactionInputs);
        }
    }
}
