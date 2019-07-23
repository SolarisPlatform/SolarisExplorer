namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public class ScriptPubKey : IScriptPubKey
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
        public long ReqSigs { get; set; }
        public string Type { get; set; }
        public string[] Addresses { get; set; }
    }
}
