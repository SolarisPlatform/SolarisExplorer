using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IClearAllDataRepository
    {
        Task ClearAllData();
    }
}