namespace Solaris.BlockExplorer.UI.Models
{
    public interface IBlockModel
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