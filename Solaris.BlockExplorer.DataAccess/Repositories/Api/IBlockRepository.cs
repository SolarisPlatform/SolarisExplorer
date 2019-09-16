using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IBlockRepository
    {
        Task<string> GetBlock(string hash);
    }
}