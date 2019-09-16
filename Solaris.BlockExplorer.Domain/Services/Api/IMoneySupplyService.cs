using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IMoneySupplyService
    {
        Task<decimal?> GetMoneySupply();
    }
}