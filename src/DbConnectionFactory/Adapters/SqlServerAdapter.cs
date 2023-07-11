using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbConnectionFactory.Adapters
{
    /// <summary>
    /// MySqlAdapter class allow get connection to MySql Server and MariaDb
    /// Assuming that connection string is right for Sql Server
    /// </summary>
    public class SqlServerAdapter : IAdapter
    {
        private readonly IConfiguration _configuration;
        private static DbConnection Connection { get; set; } = null;

        public SqlServerAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        /// <summary>
        /// Get connection for SqlServer
        /// </summary>
        /// <returns>returns connection</returns>
        /// <exception cref="SystemException">Error to connect server</exception>
        public IDbConnection GetConnection()
        {
            try
            {
                Connection.Open();
                return Connection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get connection async
        /// </summary>
        /// <returns>return connection</returns>
        public async Task<IDbConnection> GetConnectionAsync()
        {
            try
            {
                await Connection.OpenAsync();
                return Connection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get session
        /// </summary>
        /// <returns>returns the current connection, otherwise it opens the connection.</returns>
        public IDbConnection GetSession()
        {
            if (ConnectionState.Open != Connection.State)
                Connection.Open();

            return Connection;
        }
        public async Task<IDbConnection> GetSessionAsync()
        {
            if (ConnectionState.Open != Connection.State)
                await Connection.OpenAsync();

            return Connection;
        }
        /// <summary>
        /// Close connection
        /// </summary>
        public void CloseConnection()
        {
            if (ConnectionState.Open == Connection.State)
                Connection.Close();
        }
        /// <summary>
        /// Close connection async
        /// </summary>
        public Task CloseConnectionAsync()
        {
            if (ConnectionState.Open == Connection.State)
            {
                Connection.CloseAsync();
                return Task.CompletedTask;
            }
            else
                return Task.CompletedTask;
        }
    }
}
