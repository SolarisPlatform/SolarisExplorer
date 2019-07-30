using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockTransactionModelService : IBlockTransactionModelService
    {
        private readonly IBlockTransactionService _blockTransactionService;
        private readonly IMapper _mapper;
        public BlockTransactionModelService(IBlockTransactionService blockTransactionService, IMapper mapper)
        {
            _blockTransactionService = blockTransactionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IBlockTransactionModel>> GetBlockTransactions(string blockId)
        {
            var blockTransactions = await _blockTransactionService.GetBlockBlockTransactions(blockId);

            return _mapper.Map<IEnumerable<BlockTransactionModel>>(blockTransactions);
        }
    }
}
