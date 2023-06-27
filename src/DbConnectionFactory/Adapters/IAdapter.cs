using System.Data;
using System.Data.Common;

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
        IDbConnection GetSesion();
        /// <summary>
        /// Close connection
        /// </summary>
        void CloseConnection();
    }
}
