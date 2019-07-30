namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public interface IRpcVout
    {
        decimal Value { get; set; }
        long Index { get; set; }
    }
}