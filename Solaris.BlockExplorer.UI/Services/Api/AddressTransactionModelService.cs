using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services.Api;
using Solaris.BlockExplorer.UI.Models.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class AddressTransactionModelService : IAddressTransactionModelService
    {
        private readonly IAddressTransactionService _addressTransactionService;
        private readonly IMapper _mapper;

        public AddressTransactionModelService(IAddressTransactionService addressTransactionService, IMapper mapper)
        {
            _addressTransactionService = addressTransactionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressTransactionModel>> GetAddressTransactions(string publicKey, long count, long min)
        {
            var addressTransactions = await _addressTransactionService.GetAddressTransactions(publicKey, count, min);

            return _mapper.Map<IEnumerable<AddressTransactionModel>>(addressTransactions);
        }
    }
}
