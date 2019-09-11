using System.Collections.Generic;

namespace Solaris.BlockExplorer.DataAccess.Entities.Create
{
    public class OutputScriptPublicKey
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
        public long RequestedSignatures { get; set; }
        public string Type { get; set; }
        public IEnumerable<OutputScriptPublicKeyAddress> OutputScriptPublicKeyAddresses { get; set; }
    }
}
