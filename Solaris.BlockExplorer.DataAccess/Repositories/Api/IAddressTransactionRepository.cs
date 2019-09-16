using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Entities.Read.Api;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IAddressTransactionRepository
    {
        Task<IEnumerable<AddressTransaction>> GetAddressTransactions(string publicKey, long count, long min);
    }
}