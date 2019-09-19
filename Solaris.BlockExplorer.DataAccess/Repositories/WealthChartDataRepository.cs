using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class WealthChartDataRepository : IWealthChartDataRepository
    {
        private readonly IDbConnection _dbConnection;

        public WealthChartDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<WealthChartData> GetWealthChartData()
        {
            return _dbConnection.QueryFirstOrDefaultAsync<WealthChartData>("storedprocedures.GetWealthChart", commandType: CommandType.StoredProcedure);
        }
    }
}
