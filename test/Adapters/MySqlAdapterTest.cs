using DbConnectionFactory.Adapters;
using DbConnectionFactoryTests.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace DbConnectionFactoryTests.Adapters
{
    /// <summary>
    /// If Docker Desktop is installed, run the following command to start a container suitable for the tests.
    /// <code>
    /// docker run -d -p 3306:3306 --name DbConnectionFactory.Tests.MySQL -e MYSQL_DATABASE=testsMySql -e MYSQL_USER=test -e MYSQL_PASSWORD=secret -e MYSQL_ROOT_PASSWORD=secret mysql
    /// or
    /// docker run -d -p 3808:3808 --name DbConnectionFactory.Tests.MariaDb -e MARIADB_DATABASE=testsMariaDb -e MARIADB_USER=test -e MARIADB_PASSWORD=secret -e MARIADB_ROOT_PASSWORD=secret mariadb
    /// </code>
    /// </summary>

    public class MySqlAdapterTest
    {
        private MySqlAdapter? _mySqlAdapter;
        private readonly string _validConnectionString = string.Empty;
        private readonly string _invalidConnectionString = string.Empty;
        private readonly Mock<IConfigurationSection> _mockConfigurationSection;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<DbConnection> _mockDbConnection;

        public MySqlAdapterTest()
        {
            _mockConfigurationSection = new();
            _mockConfiguration = new();
            _mockDbConnection = new();
            _validConnectionString = "Server=127.0.0.1;Database=testsMySql;Uid=test;Pwd=secret;";
            _invalidConnectionString = "Server=127.0.0.1;Database=testsMySql;Uid=test;Pwd=secret1;";
            //_mockConfigurationSection
            //    .SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")])
            //    .Returns("Server=127.0.0.1;Database=ProductManagementDB;Uid=root;Pwd=secret;");

            //_mockConfiguration
            //    .Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
            //    .Returns(_mockConfigurationSection.Object);

            //_mySqlAdapter = new(_mockConfiguration.Object);
        }

        [Fact]
        public void GetConnection_ValidConnectionString_NotNulConnectionAndIsAssignableFrom()
        {
            _mockConfigurationSection.ValidSection<Mock<IConfigurationSection>>(_validConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _mySqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = _mySqlAdapter.GetConnection();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IDbConnection>(result);
        }

        [Fact]
        public void GetConnection_ValidConnectionString_ConnectionStateIsOpen()
        {
            _mockConfigurationSection.ValidSection<Mock<IConfigurationSection>>(_validConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _mySqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = _mySqlAdapter.GetConnection();

            Assert.Equal(ConnectionState.Open, result.State);
        }

        [Fact]
        public void GetConnection_InvalidConnectionString_InnerException()
        {
            _mockConfigurationSection.InValidSection<Mock<IConfigurationSection>>(_invalidConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _mySqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = Assert.Throws<MySqlException>(() => _mySqlAdapter.GetConnection());

            Assert.NotNull(result.InnerException);
            Assert.Contains("Access denied", result.InnerException.Message, StringComparison.CurrentCultureIgnoreCase);
        }

        [Fact]
        public void GetConnection_InvalidConnectionString_MySqlException()
        {
            _mockConfigurationSection.InValidSection<Mock<IConfigurationSection>>(_invalidConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _mySqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            Assert.Throws<MySqlException>(() => _mySqlAdapter.GetConnection());
        }

        [Fact]
        public void GetConnection_InvalidConnectionString_MessageMySqlException()
        {
            _mockConfigurationSection.InValidSection<Mock<IConfigurationSection>>(_invalidConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _mySqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = Assert.Throws<MySqlException>(() => _mySqlAdapter.GetConnection());

            Assert.NotNull(result.Message);
            Assert.Contains("failed with message: Access denied for user", result.Message, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
