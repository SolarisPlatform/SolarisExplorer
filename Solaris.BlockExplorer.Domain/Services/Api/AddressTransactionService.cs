using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

using Solaris.BlockExplorer.DataAccess.Repositories.Api;
using Solaris.BlockExplorer.Domain.Models.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class AddressTransactionService : IAddressTransactionService
    {
        private readonly IAddressTransactionRepository _addressTransactionRepository;
        private readonly IMapper _mapper;
        public AddressTransactionService(IAddressTransactionRepository addressTransactionRepository, IMapper mapper)
        {
            _addressTransactionRepository = addressTransactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressTransaction>> GetAddressTransactions(string publicKey, long count, long min)
        {
            var addressTransactions = await _addressTransactionRepository.GetAddressTransactions(publicKey, count, min);

            return _mapper.Map<IEnumerable<AddressTransaction>>(addressTransactions);
        }
    }
}
