using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public DifficultyService(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        public Task<decimal?> GetDifficulty()
        {
            return _difficultyRepository.GetDifficulty();
        }
    }
}
