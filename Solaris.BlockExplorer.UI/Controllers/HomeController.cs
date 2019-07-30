using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlockService _blockService;

        private readonly ICoinDataService _coinDataService;

        public HomeController(IBlockService blockService, IConfiguration configuration, ICoinDataService coinDataService) : base(configuration)
        {
            _blockService = blockService;
            _coinDataService = coinDataService;
        }

        public async Task<IActionResult> Index()
        {
            var blocks = await _blockService.GetBlocks();
            ViewBag.LastBlock = await _blockService.GetLastBlock();
            ViewBag.CoinData = await _coinDataService.GetCoinData();
            return View(blocks);
        }
    }
}
