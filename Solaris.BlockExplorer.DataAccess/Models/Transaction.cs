using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            BlockTransactions = new HashSet<BlockTransaction>();
            Inputs = new HashSet<Input>();
            TransactionInputs = new HashSet<TransactionInput>();
            TransactionOutputs = new HashSet<TransactionOutput>();
        }

        public string Id { get; set; }
        public string Hash { get; set; }
        public long Version { get; set; }
        public long Size { get; set; }
        public long Vsize { get; set; }
        public long Locktime { get; set; }
        public string BlockId { get; set; }
        public long Time { get; set; }
        public long BlockTime { get; set; }
        public long BlockOrder { get; set; }

        public virtual Block Block { get; set; }
        public virtual ICollection<BlockTransaction> BlockTransactions { get; set; }
        public virtual ICollection<Input> Inputs { get; set; }
        public virtual ICollection<TransactionInput> TransactionInputs { get; set; }
        public virtual ICollection<TransactionOutput> TransactionOutputs { get; set; }
    }
}
