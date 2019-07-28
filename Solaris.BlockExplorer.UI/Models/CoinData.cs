using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models
{
    public class CoinData
    {
        public class CurrentPrice
        {
            [JsonProperty("btc")]
            public decimal Btc { get; set; }

            [JsonProperty("usd")]
            public decimal Usd { get; set; }
        }

        public class MarketCap
        {
            [JsonProperty("btc")]
            public double Btc { get; set; }

            [JsonProperty("usd")]
            public double Usd { get; set; }
        }

        public class TotalVolume
        {
            [JsonProperty("btc")]
            public double Btc { get; set; }

            [JsonProperty("usd")]
            public double Usd { get; set; }
        }

        public class PriceChangePercentage24HInCurrency
        {
            [JsonProperty("btc")]
            public double Btc { get; set; }
        }

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

        public class RootObject : ICoinData
        {
            [JsonProperty("market_data")]
            public MarketData MarketData { get; set; }

        }
    }
}
