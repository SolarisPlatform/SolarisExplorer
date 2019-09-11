namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public class WalletRpcResponseWrapper<T> : IWalletRpcResponseWrapper<T>
    {
        public T Result { get; set; }
        public string Json { get; set; }
    }
}
