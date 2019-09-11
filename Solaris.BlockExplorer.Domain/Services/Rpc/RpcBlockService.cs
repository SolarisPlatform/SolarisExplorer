using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public class RpcBlockService : IRpcBlockService
    {
        private readonly IWalletRpcService<RpcBlock> _walletRpcService;
        private readonly IRpcBlockHashService _blockHashService;
        public RpcBlockService(IWalletRpcService<RpcBlock> walletRpcService, IRpcBlockHashService blockHashService)
        {
            _walletRpcService = walletRpcService;
            _blockHashService = blockHashService;
        }

        public async Task<IRpcBlock> GetBlock(long height)
        {
            var blockHash = await _blockHashService.GetBlockHash(height);
            var result = await _walletRpcService.Request(RpcMethods.GetBlock, blockHash);

            result.Result.Json = result.Json;
            return result.Result;
        }
    }
}
