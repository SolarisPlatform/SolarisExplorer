namespace Solaris.BlockExplorer.UI.Models
{
    public interface IScriptPubKeyModel
    {
        string Asm { get; set; }
        string Hex { get; set; }
        long ReqSigs { get; set; }
        string Type { get; set; }
        string[] Addresses { get; set; }
    }
}