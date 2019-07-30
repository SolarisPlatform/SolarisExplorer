using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public class BlockTransactionService : IBlockTransactionService
    {
        private readonly IBlockTransactionRepository _blockTransactionRepository;
        private readonly IMapper _mapper;

        public BlockTransactionService(IBlockTransactionRepository blockTransactionRepository, IMapper mapper)
        {
            _blockTransactionRepository = blockTransactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlockTransaction>> GetBlockBlockTransactions(string blockId)
        {
            var blockTransactions = await _blockTransactionRepository.GetBlockTransactions(blockId);

            return _mapper.Map<IEnumerable<BlockTransaction>>(blockTransactions);
        }
    }
}
