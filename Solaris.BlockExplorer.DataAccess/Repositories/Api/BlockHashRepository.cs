using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class BlockHashRepository : IBlockHashRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockHashRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<string> GetBlockHash(long index)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<string>("apistoredprocedures.GetBlockHash", new
            {
                Index = index
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
