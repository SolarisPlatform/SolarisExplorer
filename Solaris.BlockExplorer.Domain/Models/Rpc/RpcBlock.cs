using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class RpcBlock : IRpcBlock
    {
        [JsonProperty("height")]
        public long Height { get; set; }
        [JsonProperty("tx")]
        public string[] Transactions { get; set; }
        [JsonProperty("bits")]
        public string Bits { get; set; }
        [JsonProperty("chainwork")]
        public string Chainwork { get; set; }
        [JsonProperty("mediantime")]
        public long MedianTime { get; set; }
        [JsonProperty("merkleroot")]
        public string Merkleroot { get; set; }
        [JsonProperty("nonce")]
        public long Nonce { get; set; }
        [JsonProperty("version")]
        public long Version { get; set; }
        [JsonProperty("weight")]
        public long Weight { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("nTx")]
        public long TransactionCount { get; set; }
        [JsonProperty("difficulty")]
        public decimal Difficulty { get; set; }
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("previousblockhash")]
        public string PreviousBlock { get; set; }
    }
}
