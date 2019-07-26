using System;
using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class InputScriptSignature
    {
        public Guid InputId { get; set; }
        public string Asm { get; set; }
        public string Hex { get; set; }

        public virtual Input Input { get; set; }
    }
}
