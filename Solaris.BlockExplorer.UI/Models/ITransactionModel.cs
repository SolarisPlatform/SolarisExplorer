namespace Solaris.BlockExplorer.UI.Models
{
    public interface ITransactionModel
    {
        string Id { get; set; }
        long Size { get; set; }
        long LockTime { get; set; }
        long BlockTime { get; set; }
        decimal? TotalOutputs { get; set; }
        decimal? TotalInputs { get; set; }
        decimal Fee { get; set; }
        decimal FeePerByte { get; set; }
        bool IsReward { get; set; }
        long Confirmations { get; set; }
        long BlockHeight { get; set; }
        string BlockId { get; set; }
    }
}