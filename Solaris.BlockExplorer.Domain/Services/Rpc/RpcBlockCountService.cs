using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
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
