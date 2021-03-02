using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class BlockCountRepository : IBlockCountRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockCountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<long> GetBlockCount()
        {
            var blockCount = await _dbConnection.QuerySingleOrDefaultAsync<long?>("apistoredprocedures.GetBlockCount", commandType: CommandType.StoredProcedure);

            return blockCount ?? 0;
        }
    }
}
