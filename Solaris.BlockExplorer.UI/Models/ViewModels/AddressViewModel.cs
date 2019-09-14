using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class AddressViewModel : IAddressViewModel
    {
        public IAddressModel Address { get; set; }
        public PagedResultModel<IEnumerable<AddressTransactionModel>> Transactions { get; set; }
    }
}
