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

        private Task<IEnumerable<string>> GetBlockTransactionIds(string blockId)
        {
            return _dbConnection.QueryAsync<string>(
                    "storedprocedures.GetBlockTransactions", new
                    {
                        blockId
                    },
                    commandType: CommandType.StoredProcedure);
        }

        private Task<IEnumerable<BlockTransactionDetail>> GetBlockTransactionInputs(string transactionId)
        {
            return _dbConnection.QueryAsync<BlockTransactionDetail>(
                "storedprocedures.GetBlockTransactionInputs", new
                {
                    transactionId
                },
                commandType: CommandType.StoredProcedure);
        }
        private Task<IEnumerable<BlockTransactionDetail>> GetBlockTransactionOutputs(string transactionId)
        {
            return _dbConnection.QueryAsync<BlockTransactionDetail>(
                "storedprocedures.GetBlockTransactionOutputs", new
                {
                    transactionId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<BlockTransaction>> GetBlockTransactions(string blockId)
        {
            var blockTransactionIds = await GetBlockTransactionIds(blockId);
            var blockTransactions = new List<BlockTransaction>();

            foreach (var blockTransactionId in blockTransactionIds)
            {
                blockTransactions.Add(new BlockTransaction
                {
                    TransactionId = blockTransactionId,
                    Inputs = await GetBlockTransactionInputs(blockTransactionId),
                    Outputs = await GetBlockTransactionOutputs(blockTransactionId)
                });
            }

            return blockTransactions;
        }
    }
}
