using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IDifficultyRepository
    {
        Task<decimal?> GetDifficulty();
    }
}