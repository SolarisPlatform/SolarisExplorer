using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly IDbConnection _dbConnection;

        public BalanceRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<decimal?> GetBalance(string publicKey)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<decimal?>("apistoredprocedures.GetBalance", new
                {
                    PublicKey = publicKey
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
