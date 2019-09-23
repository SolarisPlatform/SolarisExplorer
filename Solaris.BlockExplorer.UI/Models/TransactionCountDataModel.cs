using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models
{
    public class TransactionCountDataModel
    {
        [JsonProperty("transactionCount")]
        public long TransactionCount { get; set; }
        [JsonProperty("height")]
        public long Height { get; set; }
    }
}
