using DbConnectionFactory;
using DbConnectionFactory.Adapters;
using DbConnectionFactory.Enums;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DbConnectionFactoryTest;

public class DbFactoryTest
{
    private readonly DbFactory _factory;
    
    public DbFactoryTest()
    {
        _factory = new ();
    }
    
    [Fact]
    public void CreateConnection_ServerTypeSQL_ReturnConnectionSQLServer()
    {
        var serverType = ServerType.SqlServer;

        var result = _factory.CreateConnection(serverType, It.IsAny<IConfiguration>());

        Assert.NotNull(result);
        Assert.IsType<SqlServerAdapter>(result);
    }
    
    [Fact]
    public void CreateConnection_ServerTypeMySql_ReturnConnectionMySql()
    {
        var serverType = ServerType.MySql;

        var result = _factory.CreateConnection(serverType, It.IsAny<IConfiguration>());

        Assert.NotNull(result);
        Assert.IsType<MySqlAdapter>(result);
    }

    [Fact]
    public void CreateConnection_ServerTypeMariaDb_ReturnConnectionMySql()
    {
        var serverType = ServerType.MariaDb;

        var result = _factory.CreateConnection(serverType, It.IsAny<IConfiguration>());

        Assert.NotNull(result);
        Assert.IsType<MySqlAdapter>(result);
    }

    [Fact]
    public void CreateConnection_ServerTypePostgres_ReturnConnectionPostgres()
    {
        var serverType = ServerType.PostgreSQL;

        var result = _factory.CreateConnection(serverType, It.IsAny<IConfiguration>());

        Assert.NotNull(result);
        Assert.IsType<PostgreSqlAdapter>(result);
    }

    [Fact]
    public void CreateConnection_ServerTypeNotSupported_ReturnException()
    {
        var serverType = ServerType.Oracle;

        Assert.Throws<NotSupportedException>(()=> _factory.CreateConnection(serverType, It.IsAny<IConfiguration>()));
    }
}