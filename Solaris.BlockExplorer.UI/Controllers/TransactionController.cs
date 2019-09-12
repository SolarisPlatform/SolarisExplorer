using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionModelService _transactionModelService;
        private readonly ITransactionInputModelService _transactionInputModelService;
        private readonly ITransactionOutputModelService _transactionOutputModelService;
        public TransactionController(IConfiguration configuration, ITransactionModelService transactionModelService, ITransactionInputModelService transactionInputModelService, ITransactionOutputModelService transactionOutputModelService) : base(configuration)
        {
            _transactionModelService = transactionModelService;
            _transactionInputModelService = transactionInputModelService;
            _transactionOutputModelService = transactionOutputModelService;
        }

        [Route("Transaction/{Id}", Name = "Transaction")]
        public async Task<IActionResult> Index(string id)
        {
            var transaction = await _transactionModelService.GetTransaction(id);
            var transactionInputs = await _transactionInputModelService.GetTransactionInputs(id);
            var transactionOutputs = await _transactionOutputModelService.GetTransactionOutputs(id);

            return View(new TransactionViewModel
            {
                Transaction = transaction,
                Inputs = transactionInputs,
                Outputs = transactionOutputs
            });
        }
    }
}