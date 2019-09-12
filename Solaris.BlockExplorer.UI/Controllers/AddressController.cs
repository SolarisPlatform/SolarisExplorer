using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressModelService _addressModelService;

        public AddressController(IConfiguration configuration, IAddressModelService addressModelService) : base(configuration)
        {
            _addressModelService = addressModelService;
        }

        [Route("Address/{Id}", Name = "Address")]
        public async Task<IActionResult> Index(string id)
        {
            return View(new AddressViewModel
            {
                Address = await _addressModelService.GetAddress(id)
            });
        }
    }
}