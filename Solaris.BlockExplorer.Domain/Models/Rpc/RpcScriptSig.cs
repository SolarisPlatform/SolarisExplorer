using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class RpcScriptSig : IRpcScriptSig
    {
        [JsonProperty("asm")]
        public string Asm { get; set; }
        [JsonProperty("hex")]
        public string Hex { get; set; }
    }
}
