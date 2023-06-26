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
    public class MySqlAdapater : IAdapter
    {
        private readonly IConfiguration _configuration;

        public MySqlAdapater(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get connection for MySQL
        /// </summary>
        /// <returns>return IDbConnection</returns>
        /// <exception cref="SystemException">Error to connect server</exception>
        public IDbConnection GetConnection()
        {
            try
            {
                DbConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                connection.Open();
                return connection;
            }
            catch (MySqlException)
            {
                //log
                throw new SystemException("Error to connect Server");
            }
        }
    }
}
