using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class Block
    {
        public Block()
        {
            BlockTransactions = new HashSet<BlockTransaction>();
            InversePreviousBlockNavigation = new HashSet<Block>();
            Transactions = new HashSet<Transaction>();
        }

        public string Id { get; set; }
        public long Size { get; set; }
        public long Height { get; set; }
        public long Version { get; set; }
        public string Merkleroot { get; set; }
        public long Time { get; set; }
        public long Nonce { get; set; }
        public long Weight { get; set; }
        public long MedianTime { get; set; }
        public string Bits { get; set; }
        public decimal Difficulty { get; set; }
        public string Chainwork { get; set; }
        public string PreviousBlock { get; set; }

        public virtual Block PreviousBlockNavigation { get; set; }
        public virtual ICollection<BlockTransaction> BlockTransactions { get; set; }
        public virtual ICollection<Block> InversePreviousBlockNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
