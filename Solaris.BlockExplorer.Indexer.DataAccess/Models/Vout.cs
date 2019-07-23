namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public class Vout : IVout
    {
        public decimal Value { get; set; }
        public long Index { get; set; }
        public ScriptPubKey ScriptPubKey { get; set; }
    }
}
