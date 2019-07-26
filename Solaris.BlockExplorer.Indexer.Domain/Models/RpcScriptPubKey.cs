using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Indexer.Domain.Models
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
        public string[] Addresses { get; set; }
    }
}
