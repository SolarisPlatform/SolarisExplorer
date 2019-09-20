using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class ClearAllDataRepository : IClearAllDataRepository
    {
        private readonly IDbConnection _dbConnection;

        public ClearAllDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task ClearAllData()
        {
            return _dbConnection.ExecuteAsync("storedprocedures.ClearAllData", commandType: CommandType.StoredProcedure);
        }
    }
}
