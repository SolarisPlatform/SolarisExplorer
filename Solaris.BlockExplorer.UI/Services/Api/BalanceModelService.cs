using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class BalanceModelService : IBalanceModelService
    {
        private readonly IBalanceService _balanceService;

        public BalanceModelService(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public Task<decimal?> GetBalance(string publicKey)
        {
            return _balanceService.GetBalance(publicKey);
        }
    }
}
