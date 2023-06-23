using DbConnectionFactory.Adapters;
using DbConnectionFactory.Enums;
using Microsoft.Extensions.Configuration;

namespace DbConnectionFactory
{
    public class DbFactory : IDbFactory
    {
        public IAdapter CreateConnection(ServerType type, IConfiguration configuration) {
			try
			{
				IAdapter result = type switch
				{
					ServerType.MySql => new MySqlAdapater(configuration),
					ServerType.SqlServer => new SqlServerAdapter(),
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
