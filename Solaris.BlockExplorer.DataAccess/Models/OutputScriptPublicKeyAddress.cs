using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class OutputScriptPublicKeyAddress
    {
        public Guid Id { get; set; }
        public Guid OutputId { get; set; }
        public string Address { get; set; }

        public virtual OutputScriptPublicKey Output { get; set; }
    }
}
