using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public class RpcScriptSig : IRpcScriptSig
    {
        [JsonProperty("asm")]
        public string Asm { get; set; }
        [JsonProperty("hex")]
        public string Hex { get; set; }
    }
}
