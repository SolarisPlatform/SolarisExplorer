using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface IAddressViewModel
    {
        IAddressModel Address { get; set; }
        PagedResultModel<IEnumerable<AddressTransactionModel>> Transactions { get; set; }
    }
}