using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class CurrentTotalSupplyRepository : ICurrentTotalSupplyRepository
    {
        private readonly IDbConnection _dbConnection;

        public CurrentTotalSupplyRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<decimal> GetCurrentTotalSupply()
        {
            return _dbConnection.QuerySingleAsync<decimal>("storedprocedures.GetCurrentTotalSupply", commandType: CommandType.StoredProcedure);
        }
    }
}
