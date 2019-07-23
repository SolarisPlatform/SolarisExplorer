namespace Solaris.BlockExplorer.UI.Models
{
    public class ScriptPubKeyModel : IScriptPubKeyModel
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
        public long ReqSigs { get; set; }
        public string Type { get; set; }
        public string[] Addresses { get; set; }
    }
}
