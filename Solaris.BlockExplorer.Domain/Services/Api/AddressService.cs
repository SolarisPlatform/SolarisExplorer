using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;
using Solaris.BlockExplorer.Domain.Models.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
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
    }
}
