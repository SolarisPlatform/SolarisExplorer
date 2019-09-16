using System.Threading.Tasks;
using Solaris.BlockExplorer.Domain.Models.Api;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IAddressService
    {
        Task<Address> GetAddress(string publicKey);
    }
}