using System.Collections.Generic;

namespace Solaris.BlockExplorer.Domain.Models
{
    public class BlockTransaction
    {
        public string TransactionId { get; set; }
        public IEnumerable<BlockTransactionDetail> Inputs { get; set; }
        public IEnumerable<BlockTransactionDetail> Outputs { get; set; }
        public decimal Value { get; set; }
    }
}
