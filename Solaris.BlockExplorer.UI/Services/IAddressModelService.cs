using System.Threading.Tasks;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public interface IAddressModelService
    {
        Task<IAddressModel> GetAddress(string publicKey);
    }
}