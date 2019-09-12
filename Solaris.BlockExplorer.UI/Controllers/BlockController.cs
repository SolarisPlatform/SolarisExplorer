using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;


namespace Solaris.BlockExplorer.UI.Controllers
{
    public class BlockController : BaseController
    {
        private readonly IBlockModelService _blockModelService;
        private readonly IBlockTransactionModelService _blockTransactionModelService;
        
        public BlockController(IBlockModelService blockModelService, IConfiguration configuration, IBlockTransactionModelService blockTransactionModelService) : base(configuration)
        {
            _blockModelService = blockModelService;
            _blockTransactionModelService = blockTransactionModelService;
        }

        [Route("Block/{Id}", Name = "Block")]
        public async Task<IActionResult> Index(string id)
        {
            var block = await _blockModelService.GetBlock(id);
            var transactions = await _blockTransactionModelService.GetBlockTransactions(id);

            return View(new BlockViewModel
            {
                Block = block,
                Transactions = transactions
            });
        }


    }
}