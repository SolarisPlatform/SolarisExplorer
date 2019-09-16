using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class MoneySupplyRepository : IMoneySupplyRepository
    {
        private readonly IDbConnection _dbConnection;

        public MoneySupplyRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<decimal?> GetMoneySupply()
        {
            return _dbConnection.QueryFirstOrDefaultAsync<decimal?>("apistoredprocedures.GetMoneySupply", commandType: CommandType.StoredProcedure);
        }
    }
}
