using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class RichListRepository : IRichListRepository
    {
        private readonly IDbConnection _dbConnection;

        public RichListRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<IEnumerable<RichListItem>>> GetRichList(Paging paging)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReturnValue", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@PageSize", value: paging.PageSize, dbType: DbType.Int64, direction: ParameterDirection.Input);
            parameters.Add("@PageNumber", value: paging.PageNumber, dbType: DbType.Int64, direction: ParameterDirection.Input);

            var result = _dbConnection.QueryAsync<RichListItem>("storedprocedures.GetRichList", parameters, commandType: CommandType.StoredProcedure);

            return new PagedResult<IEnumerable<RichListItem>>
            {
                Result = await result,
                CurrentPage = paging.PageNumber,
                PageSize = paging.PageSize,
                PageCount = parameters.Get<long>("@ReturnValue")
            };
        }
    }
}
