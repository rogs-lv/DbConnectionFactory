using DbConnectionFactory.Adapters;
using DbConnectionFactory.Enums;
using Microsoft.Extensions.Configuration;

namespace DbConnectionFactory
{
    public interface IDbFactory
    {
        IAdapter CreateConnection(ServerType type, IConfiguration configuration);
    }
}
