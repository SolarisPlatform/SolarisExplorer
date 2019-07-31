using System.Collections.Generic;
using System.Linq;

namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockTransactionModel : IBlockTransactionModel
    {
        public string TransactionId { get; set; }
        public IEnumerable<BlockTransactionDetailModel> Inputs { get; set; }
        public IEnumerable<BlockTransactionDetailModel> Outputs { get; set; }
        public decimal Value { get; set; }

        public decimal OutputSum
        {
            get { return Outputs.Sum(p => p.Amount) ?? 0; }
        }

        public decimal? Mined {
            get
            {
                return Outputs.Sum(p => p.Amount) - Inputs.Sum(p => p.Amount);
            }
        }
    }
}
