namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public interface IVin
    {
        string TxId { get; set; }
        long Vout { get; set; }
        ScriptSig ScriptSig { get; set; }
        long Sequence { get; set; }
        string CoinBase { get; set; }
    }
}