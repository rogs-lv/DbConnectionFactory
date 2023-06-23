using Microsoft.Extensions.Configuration;
using System.Data;
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

        public SqlServerAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get connection for SqlServer
        /// </summary>
        /// <returns>IDbConnection</returns>
        /// <exception cref="SystemException">Error to connect server</exception>
        public IDbConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {

                throw new SystemException("Error to connect SQL Server");
            }
        }
    }
}
