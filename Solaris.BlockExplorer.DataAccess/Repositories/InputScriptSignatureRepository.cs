using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class InputScriptSignatureRepository : IInputScriptSignatureRepository
    {
        private readonly IDbConnection _dbConnection;

        public InputScriptSignatureRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task Insert(Entities.Create.InputScriptSignature inputScriptSignature)
        {
            return _dbConnection.ExecuteAsync("storedprocedures.InsertInputScriptSignature", inputScriptSignature,
                commandType: CommandType.StoredProcedure);
        }
    }
}
