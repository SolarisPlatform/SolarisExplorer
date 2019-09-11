using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IDbConnection _dbConnection;

        public SearchRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<Search>> Search(string query)
        {
            return _dbConnection.QueryAsync<Search>("storedprocedures.GetSearch", new
            {
                Query = query
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
