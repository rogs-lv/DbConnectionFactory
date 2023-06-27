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
        private static DbConnection Connection { get; set; }

        public SqlServerAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
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
                Connection.Open();
                return Connection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public IDbConnection GetSesion()
        {
            if (ConnectionState.Open == Connection.State)
                return Connection;
            else
            {
                Connection.Open();
                return Connection;
            }
        }

        public void CloseConnection()
        {
            if (ConnectionState.Open == Connection.State)
                Connection.Close();
        }
    }
}
