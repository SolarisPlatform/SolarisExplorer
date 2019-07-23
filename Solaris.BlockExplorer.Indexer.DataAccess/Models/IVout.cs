namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public interface IVout
    {
        decimal Value { get; set; }
        long Index { get; set; }
        ScriptPubKey ScriptPubKey { get; set; }
    }
}