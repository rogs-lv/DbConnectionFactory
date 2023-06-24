using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace DbConnectionFactory.Adapters
{
    /// <summary>
    /// MySqlAdapter class allow get connection to Postgres
    /// Assuming that connection string is right for Sql Server
    /// </summary>
    public class PostgreSqlAdapter : IAdapter
    {
		private readonly IConfiguration _configuration;

        public PostgreSqlAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get connection for Postgres Server
        /// </summary>
        /// <returns>IDbConnection</returns>
        /// <exception cref="SystemException">Error to connect server</exception>
        public IDbConnection GetConnection()
        {
			try
			{
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                connection.Open();
                return connection;
			}
			catch (NpgsqlException)
			{

				throw new SystemException("Error to connect Postgres Server"); ;
			}
        }
    }
}
