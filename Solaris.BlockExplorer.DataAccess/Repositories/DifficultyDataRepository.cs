using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class DifficultyDataRepository : IDifficultyDataRepository
    {
        private readonly IDbConnection _dbConnection;

        public DifficultyDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<DifficultyData>> GetDifficultyData()
        {
            return _dbConnection.QueryAsync<DifficultyData>("storedprocedures.GetDifficultyData", commandType: CommandType.StoredProcedure);
        }
    }
}
