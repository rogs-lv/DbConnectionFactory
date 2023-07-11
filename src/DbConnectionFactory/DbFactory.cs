using DbConnectionFactory.Adapters;
using DbConnectionFactory.Enums;
using Microsoft.Extensions.Configuration;

namespace DbConnectionFactory
{
	/// <summary>
	/// DbFactory class allow create a connection for:
	///		* MySql/MariaDb
	///		* SqlServer
	///		* PostgreSQL
	/// </summary>
    public class DbFactory : IDbFactory
    {
        /// <summary>
        /// Create connection
        /// </summary>
        /// <param name="type">Indicate type of server</param>
        /// <param name="configuration">Allow configuration access to the configuration to read the connection string configured in the project</param>
        /// <returns>return adapter</returns>
        /// <exception cref="NotSupportedException">Database not supported or not implemented</exception>
        /// <exception cref="Exception">Error creating connection</exception>
        public IAdapter CreateConnection(ServerType type, IConfiguration configuration) {
			
			IAdapter result = type switch
			{
				ServerType.MySql or ServerType.MariaDb => new MySqlAdapter(configuration),
				ServerType.SqlServer => new SqlServerAdapter(configuration),
				ServerType.PostgreSQL => new PostgreSqlAdapter(configuration),
				_ => throw new NotSupportedException("Database not supported")
			};
			return result;
        }
    }
}
