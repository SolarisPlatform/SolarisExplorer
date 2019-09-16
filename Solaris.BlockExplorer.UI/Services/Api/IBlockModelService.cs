using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IBlockModelService
    {
        Task<string> GetBlock(string hash);
    }
}