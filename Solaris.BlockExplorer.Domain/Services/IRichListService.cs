using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface IRichListService
    {
        Task<PagedResult<IEnumerable<RichListItem>>> GetRichList(Paging paging);
    }
}