using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class TransactionInput
    {
        public string TransactionId { get; set; }
        public Guid InputId { get; set; }

        public virtual Input Input { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
