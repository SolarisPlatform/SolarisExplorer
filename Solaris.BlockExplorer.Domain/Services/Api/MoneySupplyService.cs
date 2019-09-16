using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class MoneySupplyService : IMoneySupplyService
    {
        private readonly IMoneySupplyRepository _moneySupplyRepository;

        public MoneySupplyService(IMoneySupplyRepository moneySupplyRepository)
        {
            _moneySupplyRepository = moneySupplyRepository;
        }

        public Task<decimal?> GetMoneySupply()
        {
            return _moneySupplyRepository.GetMoneySupply();
        }
    }
}
