using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetMoneySupplyController : ControllerBase
    {
        private readonly IMoneySupplyModelService _moneySupplyModelService;

        public GetMoneySupplyController(IMoneySupplyModelService moneySupplyModelService)
        {
            _moneySupplyModelService = moneySupplyModelService;
        }

        public Task<decimal?> Index()
        {
            return _moneySupplyModelService.GetMoneySupply();
        }
    }
}