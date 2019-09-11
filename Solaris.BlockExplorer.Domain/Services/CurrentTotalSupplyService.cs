using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class CurrentTotalSupplyService : ICurrentTotalSupplyService
    {
        private readonly ICurrentTotalSupplyRepository _currentTotalSupplyRepository;

        public CurrentTotalSupplyService(ICurrentTotalSupplyRepository currentTotalSupplyRepository)
        {
            _currentTotalSupplyRepository = currentTotalSupplyRepository;
        }

        public Task<decimal> GetCurrentTotalSupply()
        {
            return _currentTotalSupplyRepository.GetCurrentTotalSupply();
        }
    }
}
