using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class Output
    {
        public Output()
        {
            TransactionOutputs = new HashSet<TransactionOutput>();
        }

        public Guid Id { get; set; }
        public decimal Value { get; set; }

        public virtual OutputScriptPublicKey OutputScriptPublicKey { get; set; }
        public virtual ICollection<TransactionOutput> TransactionOutputs { get; set; }
    }
}
