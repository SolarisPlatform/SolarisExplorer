using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IDifficultyService
    {
        Task<decimal?> GetDifficulty();
    }
}