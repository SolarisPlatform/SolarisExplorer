using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface ICurrentTotalSupplyRepository
    {
        Task<decimal> GetCurrentTotalSupply();
    }
}