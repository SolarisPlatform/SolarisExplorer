using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IBlockCountModelService
    {
        Task<long> GetBlockCount();
    }
}