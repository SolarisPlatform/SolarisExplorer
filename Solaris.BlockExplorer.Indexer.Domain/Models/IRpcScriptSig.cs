namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcScriptSig
    {
        string Asm { get; set; }
        string Hex { get; set; }
    }
}