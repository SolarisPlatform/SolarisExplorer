using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Entities.Create;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IOutputScriptPublicKeyAddressRepository
    {
        Task Insert(OutputScriptPublicKeyAddress outputScriptPublicKeyAddress);
    }
}