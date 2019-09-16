using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.Api
{
    public class AddressModel
    {
        [JsonProperty("address")]
        public string PublicKey { get; set; }
        [JsonProperty("sent")]
        public decimal Sent { get; set; }
        [JsonProperty("received")]
        public decimal Received { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("mined")]
        public decimal Mined { get; set; }
        [JsonProperty("transactions")]
        public AddressTransactionModel[] Transactions { get; set; }
    }
}
