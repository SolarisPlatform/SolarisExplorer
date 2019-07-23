using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public interface IWalletRpcService<T>
    {
        Task<T> Request(string method, params object[] parameters);
    }
}