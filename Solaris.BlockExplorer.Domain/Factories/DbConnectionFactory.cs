using System.Data;
using System.Data.SqlClient;

namespace Solaris.BlockExplorer.Domain.Factories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public string ConnectionString { private get; set; }
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
