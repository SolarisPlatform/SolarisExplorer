using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class TransactionCountDataService : ITransactionCountDataService
    {
        private readonly ITransactionCountDataRepository _transactionCountDataRepository;
        private readonly IMapper _mapper;

        public TransactionCountDataService(ITransactionCountDataRepository transactionCountDataRepository, IMapper mapper)
        {
            _transactionCountDataRepository = transactionCountDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionCountData>> GetTransactionCount()
        {
            var transactionCountData = await _transactionCountDataRepository.GetTransactionCount();
            return _mapper.Map<IEnumerable<TransactionCountData>>(transactionCountData);
        }
    }
}
