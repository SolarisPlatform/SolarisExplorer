using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class Input
    {
        public Input()
        {
            TransactionInputs = new HashSet<TransactionInput>();
        }

        public Guid Id { get; set; }
        public string Coinbase { get; set; }
        public long? Sequence { get; set; }
        public string TransactionId { get; set; }
        public long? OutputIndex { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual TransactionOutput TransactionOutput { get; set; }
        public virtual InputScriptSignature InputScriptSignature { get; set; }
        public virtual ICollection<TransactionInput> TransactionInputs { get; set; }
    }
}
