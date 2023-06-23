using System.Data;

namespace DbConnectionFactory.Adapters
{
    public interface IAdapter
    {
        /// <summary>
        /// Get connection 
        /// </summary>
        /// <returns>return IDbConnection</returns>
        IDbConnection GetConnection();
    }
}
