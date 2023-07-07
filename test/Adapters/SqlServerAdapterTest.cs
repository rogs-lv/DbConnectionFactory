using DbConnectionFactory.Adapters;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data.Common;

namespace DbConnectionFactoryTests.Adapters
{
    public class SqlServerAdapterTest
    {
        private SqlServerAdapter? _sqlServerAdapter;
        private readonly string _validConnectionString = string.Empty;
        private readonly string _invalidConnectionString = string.Empty;
        private readonly Mock<IConfigurationSection> _mockConfigurationSection;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<DbConnection> _mockDbConnection;
        public SqlServerAdapterTest()
        {
            _mockConfigurationSection = new();
            _mockConfiguration = new();
            _mockDbConnection = new();
            _validConnectionString = "Server=mySQLServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            _invalidConnectionString = "Server=mySQLServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        }
    }
}
