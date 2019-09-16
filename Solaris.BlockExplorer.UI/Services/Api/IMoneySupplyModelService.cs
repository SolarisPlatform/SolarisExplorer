using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IMoneySupplyModelService
    {
        Task<decimal?> GetMoneySupply();
    }
}