using System;

namespace Solaris.BlockExplorer.DataAccess.Entities.Create
{
    public class InputScriptSignature
    {
        public Guid InputId { get; set; }
        public string Asm { get; set; }
        public string Hex { get; set; }
    }
}
