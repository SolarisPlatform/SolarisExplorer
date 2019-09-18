using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class RichListService : IRichListService
    {
        private readonly IRichListRepository _richListRepository;
        private readonly IMapper _mapper;

        public RichListService(IRichListRepository richListRepository, IMapper mapper)
        {
            _richListRepository = richListRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<IEnumerable<RichListItem>>> GetRichList(Paging paging)
        {
            var richList = await _richListRepository.GetRichList(_mapper.Map<DataAccess.Entities.Read.Paging>(paging));
            return _mapper.Map<PagedResult<IEnumerable<RichListItem>>>(richList);
        }
    }
}
