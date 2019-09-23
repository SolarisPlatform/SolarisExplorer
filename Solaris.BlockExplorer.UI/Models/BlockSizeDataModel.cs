using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockSizeDataModel
    {
        [JsonProperty("height")]
        public long Height { get; set; }
        [JsonProperty("size")]
        public decimal Size { get; set; }
    }
}
