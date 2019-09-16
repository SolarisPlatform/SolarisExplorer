using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models.Api;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IAddressModelService
    {
        Task<AddressModel> GetAddress(string publicKey);
    }
}