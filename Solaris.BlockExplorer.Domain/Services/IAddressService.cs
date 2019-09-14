using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models;

namespace Solaris.BlockExplorer.Domain.Services
{
    public interface IAddressService
    {
        Task<Address> GetAddress(string publicKey);
        Task<PagedResult<IEnumerable<AddressTransaction>>> GetTransactions(string publicKey, Paging paging);
    }
}