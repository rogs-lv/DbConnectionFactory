using System.Data;

namespace DbConnectionFactory.Adapters
{
    public class PostgreSqlAdapter : IAdapter
    {
        public IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
