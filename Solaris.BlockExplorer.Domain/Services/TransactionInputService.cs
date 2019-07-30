using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class TransactionInputService : ITransactionInputService
    {
        private readonly ITransactionInputRepository _transactionInputRepository;
        private readonly IMapper _mapper;
        public TransactionInputService(ITransactionInputRepository transactionInputRepository, IMapper mapper)
        {
            _transactionInputRepository = transactionInputRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionInput>> GetTransactionInputs(string transactionId)
        {
            var transactionInputs = await _transactionInputRepository.GetTransactionInputs(transactionId);

            return _mapper.Map<IEnumerable<TransactionInput>>(transactionInputs);
        }
    }
}
