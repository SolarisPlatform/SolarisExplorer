using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.Domain.Models;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public class RpcBlockCountService : IRpcBlockCountService
    {
        private readonly IWalletRpcService<long> _walletRpcService;

        public RpcBlockCountService(IWalletRpcService<long> walletRpcService)
        {
            _walletRpcService = walletRpcService;
        }

        public Task<long> GetBlockCount()
        {
            return _walletRpcService.Request(RpcMethods.GetBlockCount);
        }
    }
}
