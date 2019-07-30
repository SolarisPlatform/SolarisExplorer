using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.CoinDataService
{
    public class MarketData
    {
        [JsonProperty("current_price")]
        public CurrentPrice CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public MarketCap MarketCap { get; set; }

        [JsonProperty("total_volume")]
        public TotalVolume TotalVolume { get; set; }

        [JsonProperty("price_change_percentage_24h_in_currency")]
        public PriceChangePercentage24HInCurrency PriceChangePercentage24HInCurrency { get; set; }

        [JsonProperty("price_change_24h")]
        public double PriceChange24H { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24H { get; set; }
    }
}
