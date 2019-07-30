using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class TransactionViewModel : ITransactionViewModel
    {
        public ITransactionModel Transaction { get; set; }
        public IEnumerable<ITransactionInputModel> Inputs { get; set; }
        public IEnumerable<ITransactionOutputModel> Outputs { get; set; }
    }
}
