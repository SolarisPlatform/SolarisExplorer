using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.Domain.Mappings
{
    public class DomainObjectsProfile : Profile
    {
        public DomainObjectsProfile()
        {
            CreateMap<Block, Models.Block>();
            CreateMap<BlockTransaction, Models.BlockTransaction>();
            CreateMap<BlockTransactionDetail, Models.BlockTransactionDetail>();
            CreateMap<Transaction, Models.Transaction>();
            CreateMap<TransactionInput, Models.TransactionInput>();
            CreateMap<TransactionOutput, Models.TransactionOutput>();
            CreateMap<Address, Models.Address>();
        }  
    }
}
