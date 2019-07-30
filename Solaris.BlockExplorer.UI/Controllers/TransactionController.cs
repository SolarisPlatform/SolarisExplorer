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

        public async Task<IActionResult> Index(string transactionHash)
        {
            var transaction = await _transactionModelService.GetTransaction(transactionHash);
            var transactionInputs = await _transactionInputModelService.GetTransactionInputs(transactionHash);
            var transactionOutputs = await _transactionOutputModelService.GetTransactionOutputs(transactionHash);

            return View(new TransactionViewModel
            {
                Transaction = transaction,
                Inputs = transactionInputs,
                Outputs = transactionOutputs
            });
        }
    }
}