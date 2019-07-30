namespace Solaris.BlockExplorer.UI.Models
{
    public interface ITransactionInputModel
    {
        string PreviousOutputTransactionId { get; set; }
        long PreviousOutputIndex { get; set; }
        string[] Addresses { get; set; }
        decimal Amount { get; set; }
    }
}