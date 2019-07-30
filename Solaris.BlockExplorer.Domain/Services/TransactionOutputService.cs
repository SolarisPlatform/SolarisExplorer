using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class TransactionOutputService : ITransactionOutputService
    {
        private readonly ITransactionOutputRepository _transactionOutputRepository;
        private readonly IMapper _mapper;

        public TransactionOutputService(ITransactionOutputRepository transactionOutputRepository, IMapper mapper)
        {
            _transactionOutputRepository = transactionOutputRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionOutput>> GetTransactionOutputs(string transactionId)
        {
            var transactionOutputs = await _transactionOutputRepository.GetTransactionOutputs(transactionId);
            return _mapper.Map<IEnumerable<TransactionOutput>>(transactionOutputs);
        }
    }
}
