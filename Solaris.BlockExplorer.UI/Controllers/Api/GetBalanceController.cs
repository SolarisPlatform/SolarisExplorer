using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [ApiController]
    public class GetBalanceController : ControllerBase
    {
        private readonly IBalanceModelService _balanceModelService;

        public GetBalanceController(IBalanceModelService balanceModelService)
        {
            _balanceModelService = balanceModelService;
        }

        [Route("api/GetBalance/{PublicKey}", Name = "ApiGetBalance")]
        public Task<decimal?> Index(string publicKey)
        {
            return _balanceModelService.GetBalance(publicKey);
        }
    }
}