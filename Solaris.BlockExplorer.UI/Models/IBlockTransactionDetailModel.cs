namespace Solaris.BlockExplorer.UI.Models
{
    public interface IBlockTransactionDetailModel
    {
        string[] Addresses { get; set; }
        decimal? Amount { get; set; }
        bool IsNullData { get; set; }
    }
}