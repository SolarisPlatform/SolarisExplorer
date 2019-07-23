﻿using System.Threading.Tasks;
using Solaris.BlockExplorer.Indexer.Domain.Models;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
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
            var block = await _walletRpcService.Request(RpcMethods.GetBlock, blockHash);
            return block;
        }
    }
}
