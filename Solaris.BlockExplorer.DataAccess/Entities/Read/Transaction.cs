namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class Transaction
    {
        public string Id { get; set; }
        public long Size { get; set; }
        public long LockTime { get; set; }
        public long BlockTime { get; set; }
        public decimal? TotalOutputs { get; set; }
        public decimal? TotalInputs { get; set; }

        public decimal Fee
        {
            get
            {
                if (TotalInputs == null)
                    return 0;

                return (TotalInputs ?? 0) - (TotalOutputs ?? 0);
            }
        }

        public decimal FeePerByte => Fee / Size;

        public bool IsReward => (TotalInputs == null);

        public long Confirmations { get; set; }
        public long BlockHeight { get; set; }
        public string BlockId { get; set; }
        public string Json { get; set; }
    }
}
