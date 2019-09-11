using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Search>> Search(string query);
    }
}