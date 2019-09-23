using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class BlockSizeDataRepository : IBlockSizeDataRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockSizeDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<BlockSizeData>> GetBlockSizeData()
        {
            return _dbConnection.QueryAsync<BlockSizeData>("storedprocedures.GetBlockSizeData", commandType: CommandType.StoredProcedure);
        }
    }
}
