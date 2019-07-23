namespace Solaris.BlockExplorer.UI.Models
{
    public interface IVoutModel
    {
        decimal Value { get; set; }
        long Index { get; set; }
        ScriptPubKeyModel ScriptPubKey { get; set; }
    }
}