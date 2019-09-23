using System;
using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Models
{
    public class DifficultyDataModel
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        [JsonProperty("difficulty")]
        public decimal Difficulty { get; set; }
        [JsonProperty("height")]
        public long Height { get; set; }
    }
}
