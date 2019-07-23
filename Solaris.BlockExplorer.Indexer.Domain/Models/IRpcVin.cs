namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcVin
    {
        string TxId { get; set; }
        long Vout { get; set; }
        RpcScriptSig ScriptSig { get; set; }
        long Sequence { get; set; }
        string CoinBase { get; set; }
    }
}