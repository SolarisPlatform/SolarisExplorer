namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public interface IBlock
    {
        long Id { get; set; }
        string Hash { get; set; }
        long Height { get; set; }
        string[] Transactions { get; set; }
        long Time { get; set; }
        long TransactionCount { get; set; }
        decimal Difficulty { get; set; }
        long Size { get; set; }
        decimal ValueOut { get; set; }
    }
}