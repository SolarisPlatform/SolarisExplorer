using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Solaris.BlockExplorer.UI.Models;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IDifficultyDataModelService _difficultyDataModelService;
        private readonly ITransactionCountDataModelService _transactionCountDataModelService;
        private readonly IBlockSizeDataModelService _blockSizeDataModelService;
        public StatisticsController(IDifficultyDataModelService difficultyDataModelService, ITransactionCountDataModelService transactionCountDataModelService, IBlockSizeDataModelService blockSizeDataModelService)
        {
            _difficultyDataModelService = difficultyDataModelService;
            _transactionCountDataModelService = transactionCountDataModelService;
            _blockSizeDataModelService = blockSizeDataModelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public Task<IEnumerable<DifficultyDataModel>> GetDifficultyData()
        {
            return _difficultyDataModelService.GetDifficultyData();
        }
        [HttpPost]
        public Task<IEnumerable<TransactionCountDataModel>> GetTransactionCountData()
        {
            return _transactionCountDataModelService.GetTransactionCount();
        }

        [HttpPost]
        public Task<IEnumerable<BlockSizeDataModel>> GetBlockSizeData()
        {
            return _blockSizeDataModelService.GetBlockSizeData();
        }
    }
}