using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class OutputScriptPublicKey
    {
        public OutputScriptPublicKey()
        {
            OutputScriptPublicKeyAddresses = new HashSet<OutputScriptPublicKeyAddress>();
        }

        public Guid OutputId { get; set; }
        public string Asm { get; set; }
        public string Hex { get; set; }
        public long RequestedSignatures { get; set; }
        public string Type { get; set; }

        public virtual Output Output { get; set; }
        public virtual ICollection<OutputScriptPublicKeyAddress> OutputScriptPublicKeyAddresses { get; set; }
    }
}
