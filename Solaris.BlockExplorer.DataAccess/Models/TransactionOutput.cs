using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class TransactionOutput
    {
        public string TransactionId { get; set; }
        public Guid OutputId { get; set; }
        public long OutputIndex { get; set; }

        public virtual Output Output { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual Input Inputs { get; set; }
    }
}
