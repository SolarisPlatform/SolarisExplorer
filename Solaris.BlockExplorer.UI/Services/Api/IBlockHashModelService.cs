using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IBlockHashModelService
    {
        Task<string> GetBlockHash(long index);
    }
}