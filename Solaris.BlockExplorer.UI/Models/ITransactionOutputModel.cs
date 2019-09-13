namespace Solaris.BlockExplorer.UI.Models
{
    public interface ITransactionOutputModel
    {
        long Index { get; set; }
        string[] Addresses { get; set; }
        string RedeemedTransactionId { get; set; }
        decimal Amount { get; set; }
        bool IsNullData { get; set; }
    }
}