namespace Solaris.BlockExplorer.DataAccess.Entities.Create
{
    public class Block
    {
        public string Id { get; set; }
        public long Size { get; set; }
        public long Height { get; set; }
        public long Version { get; set; }
        public string Merkleroot { get; set; }
        public long Time { get; set; }
        public long Nonce { get; set; }
        public long Weight { get; set; }
        public long MedianTime { get; set; }
        public string Bits { get; set; }
        public decimal Difficulty { get; set; }
        public string Chainwork { get; set; }
        public string PreviousBlock { get; set; }
        public string Json { get; set; }
    }
}
