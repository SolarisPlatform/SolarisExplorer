using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ICurrentTotalSupplyModelService
    {
        Task<decimal> GetCurrentTotalSupply();
    }
}