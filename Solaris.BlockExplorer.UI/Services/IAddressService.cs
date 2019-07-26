using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IAddressService
    {
        Task<AddressModel> GetAddress(string address);
    }
}