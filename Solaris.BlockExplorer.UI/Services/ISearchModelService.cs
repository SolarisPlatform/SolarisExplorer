using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface ISearchModelService
    {
        Task<SearchModel[]> Search(string query);
    }
}