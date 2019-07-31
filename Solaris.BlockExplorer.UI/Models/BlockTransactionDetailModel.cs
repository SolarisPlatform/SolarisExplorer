namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockTransactionDetailModel : IBlockTransactionDetailModel
    {
        public string[] Addresses { get; set; } = new string[0];
        public decimal? Amount { get; set; }
    }
}
