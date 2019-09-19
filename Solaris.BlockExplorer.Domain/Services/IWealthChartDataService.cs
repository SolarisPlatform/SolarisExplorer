using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface IWealthChartDataService
    {
        Task<WealthChartData> GetWealthChartData();
    }
}