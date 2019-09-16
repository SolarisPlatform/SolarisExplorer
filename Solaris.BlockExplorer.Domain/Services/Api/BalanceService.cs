using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public Task<decimal?> GetBalance(string publicKey)
        {
            return _balanceRepository.GetBalance(publicKey);
        }
    }
}
