using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Models;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlockModelService _blockModelService;
        private readonly ICoinDataService _coinDataService;
        private readonly ICurrentTotalSupplyModelService _currentTotalSupplyModelService;
        private readonly IConfiguration _configuration;
        public HomeController(IBlockModelService blockModelService, IConfiguration configuration, ICoinDataService coinDataService, ICurrentTotalSupplyModelService currentTotalSupplyModelService) : base(configuration)
        {
            _blockModelService = blockModelService;
            _configuration = configuration;
            _coinDataService = coinDataService;
            _currentTotalSupplyModelService = currentTotalSupplyModelService;
        }

        public async Task<IActionResult> Index(PagingModel pagingModel)
        {
            if (pagingModel?.PageSize > 1000)
                pagingModel.PageSize = 1000;

            if (pagingModel?.PageNumber <= 0)
                pagingModel.PageSize = 0;

            var blocks = await _blockModelService.GetBlocks(pagingModel ?? new PagingModel());

            return View(new BlocksViewModel
            {
                Blocks = blocks
            });
        }

        [HttpPost]
        public async Task<ActionResult> MarketData()
        {
            var coinDataEnabled = _configuration.GetSection("CoinGecko")?.GetValue<bool?>("IsEnabled") ?? false;
            var totalSupplyEnabled = _configuration.GetValue<bool?>("IsTotalSupplyEnabled") ?? false;

            if (coinDataEnabled)
                ViewBag.CoinData = await _coinDataService.GetCoinData();

            if (totalSupplyEnabled)
                ViewBag.CurrentTotalSupply = await _currentTotalSupplyModelService.GetCurrentTotalSupply();

            ViewBag.CoinDataEnabled = coinDataEnabled;
            ViewBag.CurrentTotalSupplyEnabled = totalSupplyEnabled;

            ViewBag.BlockHeight = await _blockModelService.GetBlockHeight();

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Block(long height)
        {
            return PartialView(await _blockModelService.GetBlock(height));
        }
    }
}
