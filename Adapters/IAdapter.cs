using System.Data;

namespace DbConnectionFactory.Adapters
{
    public interface IAdapter
    {
        IDbConnection GetConnection();
    }
}
