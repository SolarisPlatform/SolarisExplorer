using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class RpcTransaction : IRpcTransaction
    {
        [JsonProperty("vin")]
        public RpcVin[] Inputs { get; set; }
        [JsonProperty("vout")]
        public RpcVout[] Outputs { get; set; }
        [JsonProperty("hex")]
        public string Hex { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("txid")]
        public string TxId { get; set; }
        [JsonProperty("version")]
        public long Version { get; set; }
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("vsize")]
        public long Vsize { get; set; }
        [JsonProperty("locktime")]
        public long Locktime { get; set; }
        [JsonProperty("blockhash")]
        public string BlockHash { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("blocktime")]
        public long BlockTime { get; set; }
    }
}
