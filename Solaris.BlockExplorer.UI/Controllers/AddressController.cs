using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService, IConfiguration configuration) : base(configuration)
        {
            _addressService = addressService;
        }

        public async Task<IActionResult> Index(string address)
        {
            return View(await _addressService.GetAddress(address));
        }
    }
}