using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Models.Api;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [ApiController]
    public class GetAddressController : ControllerBase
    {
        private readonly IAddressModelService _addressModelService;

        public GetAddressController(IAddressModelService addressModelService)
        {
            _addressModelService = addressModelService;
        }

        [Route("api/GetAddress/{PublicKey}", Name = "ApiGetAddress")]
        public Task<AddressModel> Index(string publicKey)
        {
            return _addressModelService.GetAddress(publicKey);
        }
    }
}