using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionOutputModelService : ITransactionOutputModelService
    {
        private readonly ITransactionOutputService _transactionOutputService;
        private readonly IMapper _mapper;

        public TransactionOutputModelService(ITransactionOutputService transactionOutputService, IMapper mapper)
        {
            _transactionOutputService = transactionOutputService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ITransactionOutputModel>> GetTransactionOutputs(string transactionId)
        {
            var transactionInputs = await _transactionOutputService.GetTransactionOutputs(transactionId);

            return _mapper.Map<IEnumerable<TransactionOutputModel>>(transactionInputs);
        }
    }
}
