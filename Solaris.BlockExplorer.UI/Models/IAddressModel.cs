using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models
{
    public interface IAddressModel
    {
        string Address { get; set; }
        decimal Balance { get; set; }
        decimal Received { get; set; }
        long ReceivedCount { get; set; }
        decimal Sent { get; set; }
        long SentCount { get; set; }
        decimal Mined { get; set; }
        long MinedCount { get; set; }
        IEnumerable<ITransactionModel> Transactions { get; set; }
    }
}