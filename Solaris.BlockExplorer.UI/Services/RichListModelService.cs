using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Models;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class RichListModelService : IRichListModelService
    {
        private readonly IRichListService _richListService;
        private readonly IMapper _mapper;

        public RichListModelService(IRichListService richListService, IMapper mapper)
        {
            _richListService = richListService;
            _mapper = mapper;
        }

        public async Task<PagedResultModel<IEnumerable<RichListItemModel>>> GetRichList(PagingModel paging)
        {
            var richList = await _richListService.GetRichList(_mapper.Map<Paging>(paging));
            return _mapper.Map<PagedResultModel<IEnumerable<RichListItemModel>>>(richList);
        }
    }
}
