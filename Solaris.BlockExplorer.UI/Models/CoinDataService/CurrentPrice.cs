using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.CoinDataService
{
    public class CurrentPrice
    {
        [JsonProperty("btc")]
        public decimal Btc { get; set; }

        [JsonProperty("usd")]
        public decimal Usd { get; set; }
    }
}
