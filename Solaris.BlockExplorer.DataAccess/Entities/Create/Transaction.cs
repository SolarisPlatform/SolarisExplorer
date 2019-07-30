namespace Solaris.BlockExplorer.DataAccess.Entities.Create
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Hash { get; set; }
        public long Version { get; set; }
        public long Size { get; set; }
        public long Vsize { get; set; }
        public long Locktime { get; set; }
        public string BlockId { get; set; }
        public long Time { get; set; }
        public long BlockTime { get; set; }
        public long BlockOrder { get; set; }
    }
}
