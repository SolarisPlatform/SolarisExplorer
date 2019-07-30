using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Create;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class OutputScriptPublicKeyRepository : IOutputScriptPublicKeyRepository
    {
        private readonly IDbConnection _dbConnection;

        public OutputScriptPublicKeyRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task Insert(OutputScriptPublicKey outputScriptPublicKey)
        {
            return _dbConnection.ExecuteAsync("storedprocedures.InsertOutputScriptPublicKey", outputScriptPublicKey,
                commandType: CommandType.StoredProcedure);
        }
    }
}
