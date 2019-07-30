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
            CreateMap<BlockTransactionTo, Models.BlockTransactionTo>();
            CreateMap<Transaction, Models.Transaction>();
            CreateMap<TransactionInput, Models.TransactionInput>();
            CreateMap<TransactionOutput, Models.TransactionOutput>();
        }  
    }
}
