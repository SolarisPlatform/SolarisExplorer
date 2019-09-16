using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IAddressTransactionModelService
    {
        Task<IEnumerable<AddressTransactionModel>> GetAddressTransactions(string publicKey, long count, long min);
    }
}