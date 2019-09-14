using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Address> GetAddress(string publicKey)
        {
            var address = await _addressRepository.GetAddress(publicKey);

            return _mapper.Map<Address>(address);
        }

        public async Task<PagedResult<IEnumerable<AddressTransaction>>> GetTransactions(string publicKey, Paging paging)
        {
            var transactions = await _addressRepository.GetAddressTransactions(publicKey, _mapper.Map<DataAccess.Entities.Read.Paging>(paging));
            return _mapper.Map<PagedResult<IEnumerable<AddressTransaction>>>(transactions);
        }
    }
}
