using System.Threading.Tasks;

namespace Solaris.BlockExplorer.DataAccess.Repositories.Api
{
    public interface IBalanceRepository
    {
        Task<decimal?> GetBalance(string publicKey);
    }
}