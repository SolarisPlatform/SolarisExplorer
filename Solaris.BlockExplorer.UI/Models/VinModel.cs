namespace Solaris.BlockExplorer.UI.Models
{
    public class VinModel : IVinModel
    {
        public string[] From { get; set; } = new string[0];
        public string TxId { get; set; }
        public long Vout { get; set; }
        public ScriptSigModel ScriptSig { get; set; }
        public long Sequence { get; set; }
        public string CoinBase { get; set; }
    }
}
