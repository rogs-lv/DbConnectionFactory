using System.Data;

namespace DbConnectionFactory.Adapters
{
    public interface IAdapter
    {
        /// <summary>
        /// Get connection 
        /// </summary>
        /// <returns>return new connection</returns>
        IDbConnection GetConnection();
        /// <summary>
        /// Get connection async
        /// </summary>
        /// <returns>returns new connection</returns>
        Task<IDbConnection> GetConnectionAsync();
        /// <summary>
        /// Get session
        /// </summary>
        /// <returns>returns the current connection, otherwise it opens the connection.</returns>
        IDbConnection GetSession();
        /// <summary>
        /// Get Session Async
        /// </summary>
        /// <returns>returns the current connection, otherwise it opens the connection.</returns>
        Task<IDbConnection> GetSessionAsync();
        /// <summary>
        /// Close connection
        /// </summary>
        void CloseConnection();
        /// <summary>
        /// Close connection async
        /// </summary>
        Task CloseConnectionAsync();
    }
}
