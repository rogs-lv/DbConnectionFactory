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
        /// <summary>
        /// Get session
        /// </summary>
        /// <returns></returns>
        IDbConnection GetSession();
        /// <summary>
        /// Close connection
        /// </summary>
        void CloseConnection();
    }
}
