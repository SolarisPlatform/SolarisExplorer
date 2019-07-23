using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.Domain.Models;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public class RpcBlockHashService : IRpcBlockHashService
    {
        private readonly IWalletRpcService<string> _walletRpcService;

        public RpcBlockHashService(IWalletRpcService<string> walletRpcService)
        {
            _walletRpcService = walletRpcService;
        }

        public Task<string> GetBlockHash(long height)
        {
            return _walletRpcService.Request(RpcMethods.GetBlockHash, height);
        }
    }
}
