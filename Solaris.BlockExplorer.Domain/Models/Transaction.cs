namespace Solaris.BlockExplorer.Domain.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public long Size { get; set; }
        public long LockTime { get; set; }
        public long BlockTime { get; set; }
        public decimal? TotalOutputs { get; set; }
        public decimal? TotalInputs { get; set; }
        public decimal Fee { get; set; }
        public decimal FeePerByte { get; set; }
        public bool IsReward { get; set; }
        public long Confirmations { get; set; }
        public long BlockHeight { get; set; }
        public string BlockId { get; set; }
        public string Json { get; set; }
    }
}
