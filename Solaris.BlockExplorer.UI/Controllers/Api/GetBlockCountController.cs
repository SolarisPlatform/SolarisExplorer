using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBlockCountController : ControllerBase
    {
        private readonly IBlockCountModelService _blockCountModelService;

        public GetBlockCountController(IBlockCountModelService blockCountModelService)
        {
            _blockCountModelService = blockCountModelService;
        }

        public Task<long> Index()
        {
            return _blockCountModelService.GetBlockCount();
        }
    }
}