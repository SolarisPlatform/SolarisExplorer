using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class WealthChartDataModelService : IWealthChartDataModelService
    {
        private readonly IWealthChartDataService _wealthChartDataService;
        private readonly IMapper _mapper;

        public WealthChartDataModelService(IWealthChartDataService wealthChartDataService, IMapper mapper)
        {
            _wealthChartDataService = wealthChartDataService;
            _mapper = mapper;
        }

        public async Task<WealthChartDataModel> GetWealthChartData()
        {
            var wealthChartData = await _wealthChartDataService.GetWealthChartData();

            return _mapper.Map<WealthChartDataModel>(wealthChartData);
        }
    }
}
