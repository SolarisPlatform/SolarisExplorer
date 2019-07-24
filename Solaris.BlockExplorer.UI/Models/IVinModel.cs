namespace Solaris.BlockExplorer.UI.Models
{
    public interface IVinModel
    {
        string[] From { get; set; }
        string TxId { get; set; }
        long Vout { get; set; }
        ScriptSigModel ScriptSig { get; set; }
        long Sequence { get; set; }
        string CoinBase { get; set; }
        decimal Amount { get; set; }
    }
}