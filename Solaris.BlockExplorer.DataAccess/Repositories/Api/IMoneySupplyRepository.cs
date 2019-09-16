using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IMoneySupplyRepository
    {
        Task<decimal?> GetMoneySupply();
    }
}