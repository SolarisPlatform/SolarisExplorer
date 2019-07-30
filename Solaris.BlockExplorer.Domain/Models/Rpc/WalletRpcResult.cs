using Newtonsoft.Json;

namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class WalletRpcResult<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
