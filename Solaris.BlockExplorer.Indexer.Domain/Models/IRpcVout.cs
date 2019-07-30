namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcVout
    {
        decimal Value { get; set; }
        long Index { get; set; }
    }
}