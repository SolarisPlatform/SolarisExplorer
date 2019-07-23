using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Indexer.Domain.Models
{
    public class WalletRpcResult<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
