using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class DifficultyDataModelService : IDifficultyDataModelService
    {
        private readonly IDifficultyDataService _difficultyDataService;
        private readonly IMapper _mapper;

        public DifficultyDataModelService(IDifficultyDataService difficultyDataService, IMapper mapper)
        {
            _difficultyDataService = difficultyDataService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DifficultyDataModel>> GetDifficultyData()
        {
            var difficultyData = await _difficultyDataService.GetDifficultyData();

            return _mapper.Map<IEnumerable<DifficultyDataModel>>(difficultyData);
        }
    }
}
