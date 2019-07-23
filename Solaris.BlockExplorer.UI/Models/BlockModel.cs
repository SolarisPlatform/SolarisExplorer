namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockModel : IBlockModel
    {
        public long Id { get; set; }
        public string Hash { get; set; }
        public long Height { get; set; }
        public string[] Transactions { get; set; }
        public long Time { get; set; }
        public long TransactionCount { get; set; }
        public decimal Difficulty { get; set; }
        public long Size { get; set; }
        public decimal ValueOut { get; set; }
    }
}
