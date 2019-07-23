using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public class RpcBlock : IRpcBlock
    {
        [JsonProperty("height")]
        public long Height { get; set; }
        [JsonProperty("tx")]
        public string[] Transactions { get; set; }
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
    }
}
