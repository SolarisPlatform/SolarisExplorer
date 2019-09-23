using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class DifficultyDataService : IDifficultyDataService
    {
        private readonly IDifficultyDataRepository _difficultyDataRepository;
        private readonly IMapper _mapper;

        public DifficultyDataService(IDifficultyDataRepository difficultyDataRepository, IMapper mapper)
        {
            _difficultyDataRepository = difficultyDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DifficultyData>> GetDifficultyData()
        {
            var difficultyData = await _difficultyDataRepository.GetDifficultyData();

            return _mapper.Map<IEnumerable<DifficultyData>>(difficultyData);
        }
    }
}
