using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace DbConnectionFactory.Adapters
{
    /// <summary>
    /// MySqlAdapter class allow get connecting to MySql Server and MariaDb
    /// Assuming that the connection string is correct for each of them
    /// </summary>
    public class MySqlAdapter : IAdapter
    {
        private readonly IConfiguration _configuration;
        private static DbConnection Connection { get; set; } = null;

        public MySqlAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        /// <summary>
        /// Get connection for MySQL
        /// </summary>
        /// <returns>return IDbConnection</returns>
        /// <exception cref="MySqlException">Error to connect server</exception>
        public IDbConnection GetConnection()
        {
            try
            {
                Connection.Open();
                return Connection;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Session
        /// </summary>
        /// <returns>return IDbConnection</returns>
        public IDbConnection GetSession() {
            if (ConnectionState.Open != Connection.State)
                Connection.Open();

            return Connection;
        }
        /// <summary>
        /// Close Connection
        /// </summary>
        public void CloseConnection() {
            if (ConnectionState.Open == Connection.State)
                Connection.Close();
        }

    }
}
