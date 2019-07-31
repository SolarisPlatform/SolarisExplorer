using System.Collections.Generic;
using System.Linq;

namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class BlockTransaction
    {
        public string TransactionId { get; set; }
        public IEnumerable<BlockTransactionDetail> Inputs { get; set; } = new BlockTransactionDetail[0];
        public IEnumerable<BlockTransactionDetail> Outputs { get; set; } = new BlockTransactionDetail[0];
        public decimal? Value => Outputs.Sum(output => output.Amount);

    }
}
