namespace Solaris.BlockExplorer.UI.Models
{
    public interface ITransactionModel
    {
        string TxId { get; set; }
        VinModel[] Inputs { get; set; }
        VoutModel[] Outputs { get; set; }
        string Hex { get; set; }
        string Hash { get; set; }
        long Version { get; set; }
        long Size { get; set; }
        long VSize { get; set; }
        long LockTime { get; set; }
        string BlockHash { get; set; }
        long Time { get; set; }
        long BlockTime { get; set; }
    }
}