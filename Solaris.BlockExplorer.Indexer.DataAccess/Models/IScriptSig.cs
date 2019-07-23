namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public interface IScriptSig
    {
        string Asm { get; set; }
        string Hex { get; set; }
    }
}