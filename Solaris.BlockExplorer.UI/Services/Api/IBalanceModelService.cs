using System.Threading.Tasks;

namespace Solaris.BlockExplorer.UI.Services.Api
{
    public interface IBalanceModelService
    {
        Task<decimal?> GetBalance(string publicKey);
    }
}