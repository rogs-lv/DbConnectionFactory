using DbConnectionFactory.Adapters;
using DbConnectionFactory.Enums;
using Microsoft.Extensions.Configuration;

namespace DbConnectionFactory
{
    public interface IDbFactory
    {
        /// <summary>
		/// Create connection
		/// </summary>
		/// <param name="type">Indicate type of server</param>
		/// <param name="configuration">Allow read configuration to read connection string regarding of your project</param>
		/// <returns>return adapter</returns>
        IAdapter CreateConnection(ServerType type, IConfiguration configuration);
    }
}
