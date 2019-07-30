namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public interface IRpcVin
    {
        string TxId { get; set; }
        long? Vout { get; set; }
        RpcScriptSig ScriptSignature { get; set; }
        long? Sequence { get; set; }
        string Coinbase { get; set; }
    }
}