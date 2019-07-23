namespace Solaris.BlockExplorer.UI.Models
{
    public class VoutModel : IVoutModel
    {
        public decimal Value { get; set; }
        public long Index { get; set; }
        public ScriptPubKeyModel ScriptPubKey { get; set; }
    }
}
