namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public interface IRpcBlock
    {
        long Height { get; set; }
        string[] Transactions { get; set; }
        string Bits { get; set; }
        string Chainwork { get; set; }
        long MedianTime { get; set; }
        string Merkleroot { get; set; }
        long Nonce { get; set; }
        long Version { get; set; }
        long Weight { get; set; }
        string Hash { get; set; }
        long Time { get; set; }
        long TransactionCount { get; set; }
        decimal Difficulty { get; set; }
        long Size { get; set; }
        string PreviousBlock { get; set; }
        string Json { get; set; }
    }
}