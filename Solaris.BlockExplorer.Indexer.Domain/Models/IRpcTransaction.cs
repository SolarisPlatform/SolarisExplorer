namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcTransaction
    {
        RpcVin[] Inputs { get; set; }
        RpcVout[] Outputs { get; set; }
        string Hex { get; set; }
        string Hash { get; set; }
        string TxId { get; set; }
        long Version { get; set; }
        long Size { get; set; }
        long Vsize { get; set; }
        long Locktime { get; set; }
        string BlockHash { get; set; }
        long Time { get; set; }
        long BlockTime { get; set; }
    }
}