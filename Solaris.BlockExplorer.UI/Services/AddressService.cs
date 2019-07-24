using System.Linq;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class AddressService : IAddressService
    {
        private readonly ITransactionService _transactionService;

        public AddressService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IAddressModel> GetAddress(string address)
        {
            var transactions = (await _transactionService.GetTransactionsForAddress(address)).ToArray();
            var inputs = transactions.SelectMany(p => p.Inputs).ToArray();
            var outputs = transactions.SelectMany(p => p.Outputs).ToArray();
            var mints = transactions.SelectMany(p => p.Inputs).Where(p => string.IsNullOrEmpty(p.TxId)).ToArray();

            var received = outputs.Sum(p => p.Value);
            var sent = inputs.Sum(p => p.Amount);
            var mined = mints.Sum(p => p.Amount);

            return new AddressModel
            {
                Balance =  received - sent,
                Received = received,
                Sent = sent,
                Address = address,
                Transactions = transactions,
                Mined = mined,
                MinedCount = mints.LongLength,
                ReceivedCount = outputs.LongLength,
                SentCount = inputs.LongLength
            };
        }
    }
}
