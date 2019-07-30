namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public interface IRpcScriptSig
    {
        string Asm { get; set; }
        string Hex { get; set; }
    }
}