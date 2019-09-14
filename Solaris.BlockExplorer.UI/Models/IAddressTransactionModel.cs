namespace Solaris.BlockExplorer.UI.Models
{
    public interface IAddressTransactionModel
    {
        decimal Amount { get; set; }
        decimal Balance { get; set; }
        string Id { get; set; }
        string BlockId { get; set; }
        long BlockHeight { get; set; }
        bool IsMined { get; set; }
        long Time { get; set; }
    }
}