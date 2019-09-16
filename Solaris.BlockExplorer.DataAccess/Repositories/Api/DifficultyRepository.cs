using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly IDbConnection _dbConnection;

        public DifficultyRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<decimal?> GetDifficulty()
        {
            return _dbConnection.QueryFirstOrDefaultAsync<decimal?>("apistoredprocedures.GetDifficulty", commandType: CommandType.StoredProcedure);
        }
    }
}
