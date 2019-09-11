namespace Solaris.BlockExplorer.UI.Models
{
    public interface IBlockModel
    {
        long Height { get; set; }
        string Id { get; set; }
        long Transactions { get; set; }
        long Time { get; set; }
        decimal? OutputValue { get; set; }
        decimal? InputValue { get; set; }
        long Size { get; set; }
        decimal Difficulty { get; set; }
        string Json { get; set; }
    }
}