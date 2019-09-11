using System.Collections.Generic;
using AutoMapper;

namespace Solaris.BlockExplorer.UI.Mappings
{
    public class PresentationObjectsProfile : Profile
    {
        public PresentationObjectsProfile()
        {
            CreateMap<Domain.Models.Block, Models.BlockModel>();
            CreateMap<Domain.Models.BlockTransaction, Models.BlockTransactionModel>();
            CreateMap<Domain.Models.BlockTransactionDetail, Models.BlockTransactionDetailModel>();
            CreateMap<Domain.Models.Transaction, Models.TransactionModel>();
            CreateMap<Domain.Models.TransactionInput, Models.TransactionInputModel>();
            CreateMap<Domain.Models.TransactionOutput, Models.TransactionOutputModel>();
            CreateMap<Domain.Models.Address, Models.AddressModel>();
            CreateMap<Domain.Models.Paging, Models.PagingModel>().ReverseMap();
            CreateMap<Domain.Models.Search, Models.SearchModel>();
            CreateMap<Domain.Models.PagedResult<IEnumerable<Domain.Models.Block>>, Models.PagedResultModel<IEnumerable<Models.BlockModel>>>();
        }
    }
}
