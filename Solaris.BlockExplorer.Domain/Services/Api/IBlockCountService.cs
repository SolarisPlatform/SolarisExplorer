using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IBlockCountService
    {
        Task<long> GetBlockCount();
    }
}