using System.Data;

namespace Solaris.BlockExplorer.Domain.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
