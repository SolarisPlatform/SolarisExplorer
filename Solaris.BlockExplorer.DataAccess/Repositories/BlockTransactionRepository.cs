using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class BlockTransactionRepository : IBlockTransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockTransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<BlockTransaction>> GetBlockTransactions(string blockId)
        {
            var blockTransactionItems = (await
                _dbConnection.QueryAsync<BlockTransactionItem>(
                    "storedprocedures.GetBlockTransactions", new
                    {
                        blockId
                    },
                    commandType: CommandType.StoredProcedure)).ToArray();

            var blockTransactions = new List<BlockTransaction>();

            foreach (var transaction in blockTransactionItems.GroupBy(p => p.TransactionId))
            {
                var transactionId = transaction.Key;
                var items = blockTransactionItems.Where(blockTransactionItem => blockTransactionItem.TransactionId.Equals(transaction.Key) && !string.IsNullOrEmpty(blockTransactionItem.FromAddress)).ToArray();
                var from = items.Select(blockTransactionItem => blockTransactionItem.FromAddress).Distinct().ToArray();
                var fromValue = items.FirstOrDefault()?.FromValue;
                var to = blockTransactionItems.Where(blockTransactionItem => blockTransactionItem.TransactionId.Equals(transaction.Key))
                    .Select(p => new BlockTransactionTo
                    {
                        Address =
                            blockTransactionItems
                                .Where(blockTransactionItem =>
                                    blockTransactionItem.TransactionId.Equals(transaction.Key) &&
                                    blockTransactionItem.Index.Equals(p.Index))
                                .Select(address => address.ToAddress).ToArray(),
                        Amount = p.ToValue
                    }).ToArray();

                blockTransactions.Add(new BlockTransaction
                {
                    TransactionId = transactionId,
                    From = from,
                    FromValue = fromValue,
                    To = to
                });
            }

            return blockTransactions;
        }
    }
}
