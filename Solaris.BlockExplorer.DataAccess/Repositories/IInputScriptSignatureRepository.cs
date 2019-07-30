using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IInputScriptSignatureRepository
    {
        Task Insert(Entities.Create.InputScriptSignature inputScriptSignature);
    }
}