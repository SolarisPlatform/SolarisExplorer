namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcBlock
    {
        long Height { get; set; }
        string[] Transactions { get; set; }
        string Hash { get; set; }
        long Time { get; set; }
        long TransactionCount { get; set; }
        decimal Difficulty { get; set; }
        long Size { get; set; }
    }
}