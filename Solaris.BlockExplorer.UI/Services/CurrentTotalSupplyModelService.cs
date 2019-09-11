using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services;

namespace Solaris.BlockExplorer.UI.Services
{
    public class CurrentTotalSupplyModelService : ICurrentTotalSupplyModelService
    {
        private readonly ICurrentTotalSupplyService _currentTotalSupplyService;

        public CurrentTotalSupplyModelService(ICurrentTotalSupplyService currentTotalSupplyService)
        {
            _currentTotalSupplyService = currentTotalSupplyService;
        }

        public Task<decimal> GetCurrentTotalSupply()
        {
            return _currentTotalSupplyService.GetCurrentTotalSupply();
        }
    }
}
