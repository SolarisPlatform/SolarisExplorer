namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class Block
    {
        public long Height { get; set; }
        public string Id { get; set; }
        public long Transactions { get; set; }
        public long Time { get; set; }
        public decimal? OutputValue { get; set; }
        public decimal? InputValue { get; set; }
        public long Size { get; set; }
        public decimal Difficulty { get; set; }
        public string Json { get; set; }
    }
}
