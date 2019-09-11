namespace Solaris.BlockExplorer.Domain.Models.Rpc
{
    public interface IWalletRpcResponseWrapper<T>
    {
        T Result { get; set; }
        string Json { get; set; }
    }
}