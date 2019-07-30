namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockModel : IBlockModel
    {
        public long Height { get; set; }
        public string Id { get; set; }
        public long Transactions { get; set; }
        public long Time { get; set; }
        public decimal? OutputValue { get; set; }
        public decimal? InputValue { get; set; }
        public long Size { get; set; }
    }
}
