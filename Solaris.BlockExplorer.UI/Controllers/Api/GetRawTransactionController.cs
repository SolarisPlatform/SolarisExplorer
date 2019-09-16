using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRawTransactionController : ControllerBase
    {
        private readonly IRawTransactionModelService _rawTransactionModelService;

        public GetRawTransactionController(IRawTransactionModelService rawTransactionModelService)
        {
            _rawTransactionModelService = rawTransactionModelService;
        }

        public async Task<dynamic> Index(string txId)
        {
            var json = await _rawTransactionModelService.GetRawTransaction(txId);
            if (json == null)
                return NotFound();

            var block = JsonConvert.DeserializeObject<dynamic>(json);
            return block?.result;
        }
    }
}