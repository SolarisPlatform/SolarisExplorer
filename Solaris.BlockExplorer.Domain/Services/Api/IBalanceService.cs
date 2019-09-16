using System.Threading.Tasks;

namespace Solaris.BlockExplorer.Domain.Services.Api
{
    public interface IBalanceService
    {
        Task<decimal?> GetBalance(string publicKey);
    }
}