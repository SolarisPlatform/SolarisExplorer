using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class WealthChartDataService : IWealthChartDataService
    {
        private readonly IWealthChartDataRepository _wealthChartDataRepository;
        private readonly IMapper _mapper;

        public WealthChartDataService(IWealthChartDataRepository wealthChartDataRepository, IMapper mapper)
        {
            _wealthChartDataRepository = wealthChartDataRepository;
            _mapper = mapper;
        }

        public async Task<WealthChartData> GetWealthChartData()
        {
            var wealthChartData = await _wealthChartDataRepository.GetWealthChartData();

            return _mapper.Map<WealthChartData>(wealthChartData);
        }
    }
}
