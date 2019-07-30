using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public async Task<IActionResult> Index(string blockHash)
        {
            var block = await _blockModelService.GetBlock(blockHash);
            var transactions = await _blockTransactionModelService.GetBlockTransactions(blockHash);


            return View(new BlockViewModel
            {
                Block = block,
                Transactions = transactions
            });
        }


    }
}