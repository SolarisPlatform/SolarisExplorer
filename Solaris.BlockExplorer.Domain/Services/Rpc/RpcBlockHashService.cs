using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public class RpcBlockHashService : IRpcBlockHashService
    {
        private readonly IWalletRpcService<string> _walletRpcService;

        public RpcBlockHashService(IWalletRpcService<string> walletRpcService)
        {
            _walletRpcService = walletRpcService;
        }

        public async Task<string> GetBlockHash(long height)
        {
            var result = await _walletRpcService.Request(RpcMethods.GetBlockHash, height);
            return result.Result;
        }
    }
}
