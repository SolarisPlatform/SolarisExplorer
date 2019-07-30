using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models.CoinDataService
{
    public class CoinData : ICoinData
    {
        [JsonProperty("market_data")]
        public MarketData MarketData { get; set; }
    }
}
