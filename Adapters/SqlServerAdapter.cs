using System.Data;

namespace DbConnectionFactory.Adapters
{
    public class SqlServerAdapter : IAdapter
    {
        public IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
