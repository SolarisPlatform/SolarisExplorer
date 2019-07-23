namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public class Vin : IVin
    {
        public string TxId { get; set; }
        public long Vout { get; set; }
        public ScriptSig ScriptSig { get; set; }
        public long Sequence { get; set; }
        public string CoinBase { get; set; }
    }
}
