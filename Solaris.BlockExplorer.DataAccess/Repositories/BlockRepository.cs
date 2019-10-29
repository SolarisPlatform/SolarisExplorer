using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Solaris.BlockExplorer.DataAccess.Entities.Read;


namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlockRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<IEnumerable<Block>>> GetBlocks(Paging paging)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReturnValue", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@PageSize", value: paging.PageSize, dbType: DbType.Int64, direction: ParameterDirection.Input);
            parameters.Add("@PageNumber", value: paging.PageNumber, dbType: DbType.Int64, direction: ParameterDirection.Input);

            var result = _dbConnection.QueryAsync<Block>("storedprocedures.GetBlocks", parameters, commandType: CommandType.StoredProcedure);

            return new PagedResult<IEnumerable<Block>>
            {
                Result = await result,
                CurrentPage = paging.PageNumber,
                PageSize = paging.PageSize,
                PageCount = parameters.Get<long>("@ReturnValue")
            };
        }

        public Task<Block> GetBlock(string blockId)
        {
            return _dbConnection.QuerySingleAsync<Block>("storedprocedures.GetBlock", 
                new
                {
                    blockId
                },
                commandType: CommandType.StoredProcedure);
        }

        public Task<Block> GetBlock(long height)
        {
            return _dbConnection.QuerySingleAsync<Block>("storedprocedures.GetBlockByHeight", 
                new
                {
                    height
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
