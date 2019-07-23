using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public class RpcVout : IRpcVout
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("n")]
        public long Index { get; set; }
        [JsonProperty("scriptPubKey")]
        public RpcScriptPubKey ScriptPubKey { get; set; }
    }
}
