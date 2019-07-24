using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IConfiguration _configuration;
        private readonly IBlockService _blockService;
        public TransactionService(IMongoDatabase mongoDatabase, IConfiguration configuration, IBlockService blockService)
        {
            _mongoDatabase = mongoDatabase;
            _configuration = configuration;
            _blockService = blockService;
        }

        public async Task<IEnumerable<ITransactionModel>> GetTransactions(string blockHash)
        {
            var collection = GetCollection();
            var result = await collection.FindAsync(p => p.BlockHash.Equals(blockHash));

            var transactions = await result.ToListAsync();

            foreach (var transaction in transactions)
            {
                await SetInputs(transaction);
                await SetRedeemed(transaction);
            }

            return transactions;
        }

        public async Task<ITransactionModel> GetTransaction(string transactionHash, bool recursive = false)
        {
            var collection = GetCollection();
            var result = await collection.FindAsync(p => p.TxId.Equals(transactionHash));

            var transaction = await result.FirstOrDefaultAsync();

            if (transaction != null && !recursive)
            {
                await SetInputs(transaction);
                await SetRedeemed(transaction);
            }

            if (transaction != null)
            {
                var block = await _blockService.GetBlock(transaction.BlockHash);
                if (block != null)
                {
                    transaction.Block = block;
                    
                    var lastBlock = await _blockService.GetLastBlock();
                    if (lastBlock != null)
                    {
                        transaction.Confirmations = lastBlock.Height - transaction.Block.Height;
                    }
                }
            }

            return transaction;
        }

        private async Task SetInputs(ITransactionModel transaction)
        {
            foreach (var input in transaction.Inputs)
            {
                var sourceTransaction = await GetTransaction(input.TxId, true);

                var sourceOutput = sourceTransaction?.Outputs.FirstOrDefault(p => p.Index.Equals(input.Vout));

                if (sourceOutput == null)
                    continue;

                input.From = sourceOutput.ScriptPubKey.Addresses;
                input.Amount = sourceOutput.Value;
            }
        }

        private async Task SetRedeemed(ITransactionModel transaction)
        {
            var collection = GetCollection();

            foreach (var output in transaction.Outputs)
            {
                var result = await collection.FindAsync(p => p.Inputs.Any(input => input.TxId.Equals(transaction.TxId) && input.Vout.Equals(output.Index)));
                var redeemTransaction = await result.FirstOrDefaultAsync();
                output.RedeemedTxId = redeemTransaction?.TxId;
                output.RedeemedIndex = output.Index;
            }
        }

        private IMongoCollection<TransactionModel> GetCollection()
        {
            var section = _configuration.GetSection("MongoDB");
            var transactionCollectionName = section.GetValue<string>("TransactionCollectionName");

            return _mongoDatabase.GetCollection<TransactionModel>(transactionCollectionName);
        }
    }
}
