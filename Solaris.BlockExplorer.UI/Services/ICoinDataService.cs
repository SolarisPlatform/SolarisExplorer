using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models.CoinDataService;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ICoinDataService
    {
        Task<ICoinData> GetCoinData();
    }
}