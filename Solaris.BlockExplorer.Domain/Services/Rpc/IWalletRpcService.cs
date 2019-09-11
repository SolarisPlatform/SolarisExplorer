using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public interface IWalletRpcService<T>
    {
        Task<IWalletRpcResponseWrapper<T>> Request(string method, params object[] parameters);
    }
}