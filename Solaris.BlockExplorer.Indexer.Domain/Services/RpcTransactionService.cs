﻿using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.Domain.Models;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
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
            var transaction = await _walletRpcService.Request(RpcMethods.GetRawTransaction, txId, 1);
            return transaction;
        }
    }
}
