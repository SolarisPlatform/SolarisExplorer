using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.CoinDataService
{
    public class MarketCap
    {
        [JsonProperty("btc")]
        public double Btc { get; set; }

        [JsonProperty("usd")]
        public double Usd { get; set; }
    }
}
