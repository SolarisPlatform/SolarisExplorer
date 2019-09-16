using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IBlockHashService
    {
        Task<string> GetBlockHash(long index);
    }
}