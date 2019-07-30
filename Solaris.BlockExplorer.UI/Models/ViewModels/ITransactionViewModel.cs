using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface ITransactionViewModel
    {
        ITransactionModel Transaction { get; set; }
        IEnumerable<ITransactionInputModel> Inputs { get; set; }
        IEnumerable<ITransactionOutputModel> Outputs { get; set; }
    }
}