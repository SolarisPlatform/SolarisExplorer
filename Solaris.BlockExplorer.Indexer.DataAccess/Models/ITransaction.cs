namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public interface ITransaction
    {
        Vin[] Inputs { get; set; }
        Vout[] Outputs { get; set; }
        string Hex { get; set; }
        string Hash { get; set; }
        string TxId { get; set; }
        long Version { get; set; }
        long Size { get; set; }
        long VSize { get; set; }
        long LockTime { get; set; }
        string BlockHash { get; set; }
        long Time { get; set; }
        long BlockTime { get; set; }
    }
}
