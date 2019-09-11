using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class SearchModelService : ISearchModelService
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchModelService(ISearchService searchService, IMapper mapper)
        {
            _searchService = searchService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SearchModel>> Search(string query)
        {
            return _mapper.Map<IEnumerable<SearchModel>>(await _searchService.Search(query));
        }
    }
}
