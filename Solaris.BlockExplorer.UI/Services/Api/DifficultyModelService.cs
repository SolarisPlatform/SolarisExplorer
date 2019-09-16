using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Services.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public class DifficultyModelService : IDifficultyModelService
    {
        private readonly IDifficultyService _difficultyService;

        public DifficultyModelService(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }

        public Task<decimal?> GetDifficulty()
        {
            return _difficultyService.GetDifficulty();
        }
    }
}
