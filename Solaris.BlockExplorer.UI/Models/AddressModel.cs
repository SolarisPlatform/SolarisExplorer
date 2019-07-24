using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models
{
    public class AddressModel : IAddressModel
    {
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal Received { get; set; }
        public long ReceivedCount { get; set; }
        public decimal Sent { get; set; }
        public long SentCount { get; set; }
        public decimal Mined { get; set; }
        public long MinedCount { get; set; }
        public IEnumerable<ITransactionModel> Transactions { get; set; }
    }
}
