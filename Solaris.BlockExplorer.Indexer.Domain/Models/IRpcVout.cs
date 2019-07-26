namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcVout
    {
        decimal Value { get; set; }
        long OutputIndex { get; set; }
    }
}