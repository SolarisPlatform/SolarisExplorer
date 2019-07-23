using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public class RpcVin : IRpcVin
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }
        [JsonProperty("vout")]
        public long Vout { get; set; }
        [JsonProperty("scriptSig")]
        public RpcScriptSig ScriptSig { get; set; }
        [JsonProperty("sequence")]
        public long Sequence { get; set; }
        [JsonProperty("coinbase")]
        public string CoinBase { get; set; }
    }
}
