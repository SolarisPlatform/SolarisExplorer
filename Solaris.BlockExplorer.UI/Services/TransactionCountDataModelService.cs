using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionCountDataModelService : ITransactionCountDataModelService
    {
        private readonly ITransactionCountDataService _transactionCountDataService;
        private readonly IMapper _mapper;

        public TransactionCountDataModelService(ITransactionCountDataService transactionCountDataService, IMapper mapper)
        {
            _transactionCountDataService = transactionCountDataService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionCountDataModel>> GetTransactionCount()
        {
            var transactionCountData = await _transactionCountDataService.GetTransactionCount();
            return _mapper.Map<IEnumerable<TransactionCountDataModel>>(transactionCountData);
        }
    }
}
