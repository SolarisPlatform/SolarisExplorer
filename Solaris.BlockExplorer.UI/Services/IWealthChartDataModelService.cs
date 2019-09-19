using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IWealthChartDataModelService
    {
        Task<WealthChartDataModel> GetWealthChartData();
    }
}