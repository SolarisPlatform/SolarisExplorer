using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface ICurrentTotalSupplyService
    {
        Task<decimal> GetCurrentTotalSupply();
    }
}