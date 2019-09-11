using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;


namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Entities.Read.PagedResult<IEnumerable<Entities.Read.Block>>> GetBlocks(Entities.Read.Paging paging)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReturnValue", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@PageSize", value: paging.PageSize, dbType: DbType.Int64, direction: ParameterDirection.Input);
            parameters.Add("@PageNumber", value: paging.PageNumber, dbType: DbType.Int64, direction: ParameterDirection.Input);

            var result = _dbConnection.QueryAsync<Entities.Read.Block>("storedprocedures.GetBlocks", parameters, commandType: CommandType.StoredProcedure);

            return new Entities.Read.PagedResult<IEnumerable<Entities.Read.Block>>
            {
                Result = await result,
                CurrentPage = paging.PageNumber,
                PageSize = paging.PageSize,
                PageCount = parameters.Get<long>("@ReturnValue")
            };
        }

        public Task<Entities.Read.Block> GetBlock(string blockId)
        {
            return _dbConnection.QuerySingleAsync<Entities.Read.Block>("storedprocedures.GetBlock", 
                new
                {
                    blockId
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task<long> GetBlockHeight()
        {
            return _dbConnection.ExecuteScalarAsync<long>("SELECT functions.GetBlockHeight()");
        }

        public Task Insert(Entities.Create.Block block)
        {
            return _dbConnection.ExecuteAsync("storedprocedures.InsertBlock", block,
                commandType: CommandType.StoredProcedure);
        }
    }
}
