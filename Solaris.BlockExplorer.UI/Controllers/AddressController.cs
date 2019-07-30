using Microsoft.Extensions.Configuration;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class AddressController : BaseController
    {
        //private readonly IAddressService _addressService;

        public AddressController(IConfiguration configuration) : base(configuration)
        {

        }

        //public async Task<IActionResult> Index(string address)
        //{
        //    return View(await _addressService.GetAddress(address));
        //}
    }
}