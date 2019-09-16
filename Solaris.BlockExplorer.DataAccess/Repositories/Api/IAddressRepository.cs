using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Entities.Read.Api;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IAddressRepository
    {
        Task<Address> GetAddress(string publicKey);
    }
}