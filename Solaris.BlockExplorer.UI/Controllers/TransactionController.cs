using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService, IConfiguration configuration) : base(configuration)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index(string transactionHash)
        {
            var transaction = await _transactionService.GetTransaction(transactionHash);
            return View(transaction);
        }
    }
}