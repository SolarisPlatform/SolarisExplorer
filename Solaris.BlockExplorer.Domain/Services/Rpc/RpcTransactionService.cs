using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Rpc;

namespace Solaris.BlockExplorer.Domain.Services.Rpc
{
    public class RpcTransactionService : IRpcTransactionService
    {
        private readonly IWalletRpcService<RpcTransaction> _walletRpcService;

        public RpcTransactionService(IWalletRpcService<RpcTransaction> walletRpcService)
        {
            _walletRpcService = walletRpcService;
        }

        public async Task<IRpcTransaction> GetTransaction(string txId)
        {
            var result = await _walletRpcService.Request(RpcMethods.GetRawTransaction, txId, 1);
            if (result?.Result == null)
                return null;

            result.Result.Json = result.Json;
            return result.Result;
        }
    }
}
