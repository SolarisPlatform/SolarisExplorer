using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.CoinDataService
{
    public class PriceChangePercentage24HInCurrency
    {
        [JsonProperty("btc")]
        public double Btc { get; set; }
    }
}
