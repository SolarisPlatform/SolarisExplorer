using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.Api
{
    public class AddressTransactionModel
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("blockHash")]
        public string BlockId { get; set; }
        [JsonProperty("blockHeight")]
        public long BlockHeight { get; set; }
        [JsonProperty("isMined")]
        public bool IsMined { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
