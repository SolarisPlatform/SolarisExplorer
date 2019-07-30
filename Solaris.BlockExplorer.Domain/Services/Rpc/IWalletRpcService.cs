using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public interface IWalletRpcService<T>
    {
        Task<T> Request(string method, params object[] parameters);
    }
}