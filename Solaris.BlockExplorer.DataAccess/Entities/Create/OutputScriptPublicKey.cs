using System;

namespace Solaris.BlockExplorer.DataAccess.Entities.Create
{
    public class OutputScriptPublicKey
    {
        public Guid OutputId { get; set; }
        public string Asm { get; set; }
        public string Hex { get; set; }
        public long RequestedSignatures { get; set; }
        public string Type { get; set; }
    }
}
