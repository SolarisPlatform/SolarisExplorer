using AutoMapper;

namespace Solaris.BlockExplorer.UI.Mappings
{
    public class PresentationObjectsProfile : Profile
    {
        public PresentationObjectsProfile()
        {
            CreateMap<Domain.Models.Block, Models.BlockModel>();
            CreateMap<Domain.Models.BlockTransaction, Models.BlockTransactionModel>();
            CreateMap<Domain.Models.BlockTransactionTo, Models.BlockTransactionToModel>();
            CreateMap<Domain.Models.Transaction, Models.TransactionModel>();
            CreateMap<Domain.Models.TransactionInput, Models.TransactionInputModel>();
            CreateMap<Domain.Models.TransactionOutput, Models.TransactionOutputModel>();
        }
    }
}
