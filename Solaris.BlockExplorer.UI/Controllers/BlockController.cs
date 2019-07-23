using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Models;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class BlockController : BaseController
    {
        private readonly IBlockService _blockService;
        private readonly ITransactionService _transactionService;

        public BlockController(IBlockService blockService, ITransactionService transactionService, IConfiguration configuration) : base(configuration)
        {
            _blockService = blockService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index(string blockHash)
        {
            var blockTransactionsModel = new BlockTransactionsModel
            {
                Block = await _blockService.GetBlock(blockHash),
                Transactions = await _transactionService.GetTransactions(blockHash)
            };

            return View(blockTransactionsModel);
        }
    }
}