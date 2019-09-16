using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<string> GetBlock(string hash)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<string>("apistoredprocedures.GetBlock", new
            {
                Hash = hash
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
