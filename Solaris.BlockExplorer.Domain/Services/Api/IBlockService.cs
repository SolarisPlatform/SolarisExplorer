using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IBlockService
    {
        Task<string> GetBlock(string hash);
    }
}