using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solaris.BlockExplorer.DataAccess.Models;


namespace Solaris.BlockExplorer.UI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly SolarisExplorerContext _solarisExplorerContext;

        public TransactionService(SolarisExplorerContext solarisExplorerContext)
        {
            _solarisExplorerContext = solarisExplorerContext;
        }

        public Task<Transaction[]> GetTransactions(string blockHash)
        {
            return _solarisExplorerContext.Transactions.Where(p => p.BlockId.Equals(blockHash)).ToArrayAsync();
        }

        public Task<Transaction[]> GetTransactionsForAddress(string address)
        {
            return _solarisExplorerContext.TransactionOutputs.Where(p =>
                p.Output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Any(outputAddress =>
                    outputAddress.Address == address))
                .Select(p => p.Transaction)
                .ToArrayAsync();
        }

        public Task<Transaction> GetTransaction(string transactionHash)
        {
            return _solarisExplorerContext.Transactions.FirstOrDefaultAsync(p => p.Id.Equals(transactionHash));
        }
    }
}
