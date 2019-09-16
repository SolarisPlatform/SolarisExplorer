using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IDifficultyModelService
    {
        Task<decimal?> GetDifficulty();
    }
}