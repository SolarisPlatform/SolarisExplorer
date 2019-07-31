using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class RpcScriptPubKey : IRpcScriptPubKey
    {
        [JsonProperty("asm")]
        public string Asm { get; set; }
        [JsonProperty("hex")]
        public string Hex { get; set; }
        [JsonProperty("reqSigs")]
        public long RequestedSignatures { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("addresses")]
        public string[] Addresses { get; set; } = new string[0];
    }
}
