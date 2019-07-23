using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlockService _blockService;

        public HomeController(IBlockService blockService, IConfiguration configuration) : base(configuration)
        {
            _blockService = blockService;
        }

        public async Task<IActionResult> Index()
        {
            var blocks = await _blockService.GetBlocks();
            return View(blocks);
        }
    }
}
