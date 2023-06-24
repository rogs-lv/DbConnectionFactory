using DbConnectionFactory.Adapters;
using DbConnectionFactory.Enums;
using Microsoft.Extensions.Configuration;

namespace DbConnectionFactory
{
	/// <summary>
	/// DbFactory class allow create a connection for:
	///		* MySql/MariaDb
	///		* SqlServer
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
			try
			{
				IAdapter result = type switch
				{
					ServerType.MySql | ServerType.MariaDb => new MySqlAdapater(configuration),
					ServerType.SqlServer => new SqlServerAdapter(configuration),
					ServerType.PostgreSQL => new PostgreSqlAdapter(configuration),
					_ => throw new NotSupportedException("Database not supported")
				};
				return result;
			}
			catch (Exception)
			{
				//log
				throw new Exception("Can not created db connection");
			}
        }
    }
}
