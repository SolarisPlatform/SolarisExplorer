using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Api;


namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IAddressTransactionService
    {
        Task<IEnumerable<AddressTransaction>> GetAddressTransactions(string publicKey, long count, long min);
    }
}