using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models
{
    public class WealthChartDataModel
    {
        [JsonProperty("top1")]
        public decimal Top1 { get; set; }
        [JsonProperty("top2")]
        public decimal Top2 { get; set; }
        [JsonProperty("top3")]
        public decimal Top3 { get; set; }
        [JsonProperty("top4")]
        public decimal Top4 { get; set; }

        [JsonProperty("top1amount")]
        public decimal Top1Amount { get; set; }
        [JsonProperty("top2amount")]
        public decimal Top2Amount { get; set; }
        [JsonProperty("top3amount")]
        public decimal Top3Amount { get; set; }
        [JsonProperty("top4amount")]
        public decimal Top4Amount { get; set; }
    }
}
