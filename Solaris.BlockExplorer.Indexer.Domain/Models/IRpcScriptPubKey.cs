namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public interface IRpcScriptPubKey
    {
        string Asm { get; set; }
        string Hex { get; set; }
        long RequestedSignatures { get; set; }
        string Type { get; set; }
        string[] Addresses { get; set; }
    }
}