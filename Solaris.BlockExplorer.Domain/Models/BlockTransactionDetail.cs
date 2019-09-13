namespace Solaris.BlockExplorer.Domain.Models
{
    public class BlockTransactionDetail : IBlockTransactionDetail
    {
        public string[] Addresses { get; set; } = new string[0];
        public decimal? Amount { get; set; }
        public bool IsNullData { get; set; }
    }
}
