using System.Collections.Generic;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;
using Solaris.BlockExplorer.Domain.Mappings.Converters;

namespace Solaris.BlockExplorer.Domain.Mappings
{
    public class DomainObjectsProfile : Profile
    {
        public DomainObjectsProfile()
        {
            CreateMap<Block, Models.Block>();
            CreateMap<BlockTransaction, Models.BlockTransaction>();
            CreateMap<BlockTransactionDetail, Models.BlockTransactionDetail>().ForMember(p => p.Addresses, expression => expression.ConvertUsing(new AddressesConverter()));
            CreateMap<Transaction, Models.Transaction>();
            CreateMap<TransactionInput, Models.TransactionInput>().ForMember(p => p.Addresses, expression => expression.ConvertUsing(new AddressesConverter()));
            CreateMap<TransactionOutput, Models.TransactionOutput>().ForMember(p => p.Addresses, expression => expression.ConvertUsing(new AddressesConverter()));
            CreateMap<Address, Models.Address>();
            CreateMap<Paging, Models.Paging>().ReverseMap();
            CreateMap<Search, Models.Search>();
            CreateMap<AddressTransaction, Models.AddressTransaction>();
            CreateMap<RichListItem, Models.RichListItem>().ForMember(p => p.Addresses, expression => expression.ConvertUsing(new AddressesConverter()));
            CreateMap<PagedResult<IEnumerable<Block>>, Models.PagedResult<IEnumerable<Models.Block>>>();
            CreateMap<PagedResult<IEnumerable<AddressTransaction>>, Models.PagedResult<IEnumerable<Models.AddressTransaction>>>();
            CreateMap<PagedResult<IEnumerable<RichListItem>>, Models.PagedResult<IEnumerable<Models.RichListItem>>>();
            CreateMap<DataAccess.Entities.Read.Api.Address, Models.Api.Address>();
            CreateMap<DataAccess.Entities.Read.Api.AddressTransaction, Models.Api.AddressTransaction>();
            CreateMap<WealthChartData, Models.WealthChartData>();
        }  
    }
}

