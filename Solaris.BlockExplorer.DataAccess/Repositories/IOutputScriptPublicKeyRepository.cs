using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Entities.Create;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IOutputScriptPublicKeyRepository
    {
        Task Insert(OutputScriptPublicKey outputScriptPublicKey);
    }
}