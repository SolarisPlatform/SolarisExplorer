using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class RpcVout : IRpcVout
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("n")]
        public long Index { get; set; }
        [JsonProperty("scriptPubKey")]
        public RpcScriptPubKey ScriptPublicKey { get; set; }
    }
}
