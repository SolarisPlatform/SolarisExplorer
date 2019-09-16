using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class MoneySupplyModelService : IMoneySupplyModelService
    {
        private readonly IMoneySupplyService _moneySupplyService;

        public MoneySupplyModelService(IMoneySupplyService moneySupplyService)
        {
            _moneySupplyService = moneySupplyService;
        }

        public Task<decimal?> GetMoneySupply()
        {
            return _moneySupplyService.GetMoneySupply();
        }
    }
}
