using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBlockHashController : ControllerBase
    {
        private readonly IBlockHashModelService _blockHashModelService;

        public GetBlockHashController(IBlockHashModelService blockHashModelService)
        {
            _blockHashModelService = blockHashModelService;
        }

        public Task<string> Index(long index)
        {
            return _blockHashModelService.GetBlockHash(index);
        }
    }
}