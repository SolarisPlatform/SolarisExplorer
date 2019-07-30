using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<Entities.Read.Block>> GetBlocks()
        {
            return _dbConnection.QueryAsync<Entities.Read.Block>("storedprocedures.GetBlocks", commandType: CommandType.StoredProcedure);
        }

        public Task<Entities.Read.Block> GetBlock(string blockId)
        {
            return _dbConnection.QuerySingleAsync<Entities.Read.Block>("storedprocedures.GetBlock", 
                new
                {
                    blockId
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task<long> GetBlockHeight()
        {
            return _dbConnection.ExecuteScalarAsync<long>("SELECT functions.GetBlockHeight()");
        }

        public Task Insert(Entities.Create.Block block)
        {
            return _dbConnection.ExecuteAsync("storedprocedures.InsertBlock", block,
                commandType: CommandType.StoredProcedure);
        }
    }
}
