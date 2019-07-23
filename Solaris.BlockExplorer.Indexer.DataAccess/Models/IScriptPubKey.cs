namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public interface IScriptPubKey
    {
        string Asm { get; set; }
        string Hex { get; set; }
        long ReqSigs { get; set; }
        string Type { get; set; }
        string[] Addresses { get; set; }
    }
}