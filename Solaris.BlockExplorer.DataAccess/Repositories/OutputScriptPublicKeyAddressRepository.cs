using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Create;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class OutputScriptPublicKeyAddressRepository : IOutputScriptPublicKeyAddressRepository
    {
        private readonly IDbConnection _dbConnection;

        public OutputScriptPublicKeyAddressRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task Insert(OutputScriptPublicKeyAddress outputScriptPublicKeyAddress)
        {
            return _dbConnection.ExecuteAsync("storedprocedures.InsertOutputScriptPublicKeyAddress",
                outputScriptPublicKeyAddress, commandType: CommandType.StoredProcedure);
        }
    }
}
