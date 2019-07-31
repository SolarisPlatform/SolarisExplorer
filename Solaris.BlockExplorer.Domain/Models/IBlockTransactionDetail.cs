namespace Solaris.BlockExplorer.Domain.Models
{
    public interface IBlockTransactionDetail
    {
        string[] Addresses { get; set; }
        decimal? Amount { get; set; }
    }
}