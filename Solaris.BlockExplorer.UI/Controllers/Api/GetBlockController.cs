using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBlockController : ControllerBase
    {
        private readonly IBlockModelService _blockModelService;

        public GetBlockController(IBlockModelService blockModelService)
        {
            _blockModelService = blockModelService;
        }


        public async Task<dynamic> Index(string hash)
        {
            var json = await _blockModelService.GetBlock(hash);
            if (json == null)
                return NotFound();

            var block = JsonConvert.DeserializeObject<dynamic>(json);
            return block?.result;
        }
    }
}