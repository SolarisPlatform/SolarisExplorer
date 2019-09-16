using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services.Api;
using Solaris.BlockExplorer.UI.Models.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class AddressModelService : IAddressModelService
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressModelService(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        public async Task<AddressModel> GetAddress(string publicKey)
        {
            var address = await _addressService.GetAddress(publicKey);

            return _mapper.Map<AddressModel>(address);
        }
    }
}
