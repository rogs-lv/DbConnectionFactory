using DbConnectionFactory.Adapters;
using DbConnectionFactoryTests.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace DbConnectionFactoryTests.Adapters
{
    /// <summary>
    /// If Docker Desktop is installed, run the following command to start a container suitable for the tests.
    /// <code>
    /// docker run -d -p 5432:5432 --name DbConnectionFactory.Tests.Postgres -e POSTGRES_DB=testsPostgresDb -e POSTGRES_USER=test -e POSTGRES_PASSWORD=secret postgres
    /// </code>
    /// </summary>
    public class PostgreSqlAdapterTest
    {
        private PostgreSqlAdapter? _postgreSqlAdapter;
        private readonly string _validConnectionString = string.Empty;
        private readonly string _invalidConnectionString = string.Empty;
        private readonly Mock<IConfigurationSection> _mockConfigurationSection;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<DbConnection> _mockDbConnection;
        public PostgreSqlAdapterTest()
        {
            _mockConfigurationSection = new();
            _mockConfiguration = new();
            _mockDbConnection = new();
            _validConnectionString = "Server=127.0.0.1;Port=5432;Database=testsPostgresDb;User Id=test;Password=secret;";
            _invalidConnectionString = "Server=127.0.0.1;Port=5432;Database=testsPostgresDb;User Id=test;Password=secret1;";
        }

        [Fact]
        public void GetConnection_ValidConnectionString_NotNulConnectionAndIsAssignableFrom()
        {
            _mockConfigurationSection.ValidSection<Mock<IConfigurationSection>>(_validConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _postgreSqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = _postgreSqlAdapter.GetConnection();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IDbConnection>(result);
        }

        [Fact]
        public void GetConnection_ValidConnectionString_ConnectionStateIsOpen()
        {
            _mockConfigurationSection.ValidSection<Mock<IConfigurationSection>>(_validConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _postgreSqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = _postgreSqlAdapter.GetConnection();

            Assert.Equal(ConnectionState.Open, result.State);
        }

        [Fact]
        public void GetConnection_InvalidConnectionString_NotEmptyMessage()
        {
            _mockConfigurationSection.InValidSection<Mock<IConfigurationSection>>(_invalidConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _postgreSqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = Assert.Throws<PostgresException>(() => _postgreSqlAdapter.GetConnection());

            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public void GetConnection_InvalidConnectionString_NpgsqlException()
        {
            _mockConfigurationSection.InValidSection<Mock<IConfigurationSection>>(_invalidConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _postgreSqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            Assert.Throws<PostgresException>(() => _postgreSqlAdapter.GetConnection());
        }

        [Fact]
        public void GetConnection_InvalidConnectionString_NotEmptyMessageText()
        {
            _mockConfigurationSection.InValidSection<Mock<IConfigurationSection>>(_invalidConnectionString);
            _mockConfiguration.ValidConfiguration<Mock<IConfiguration>>(_mockConfigurationSection);
            _postgreSqlAdapter = new(_mockConfiguration.Object);

            _mockDbConnection.Setup(d => d.Open());

            var result = Assert.Throws<PostgresException>(() => _postgreSqlAdapter.GetConnection());

            Assert.NotNull(result);
            Assert.NotEmpty(result.MessageText);
        }

        [Fact]
        public void Exception_NetworkError_ReturnNpgsqlException()
        {
            Action error = () => { throw new NpgsqlException("Error", new Exception("Network error")); };

            var exception = Record.Exception(error);

            Assert.NotNull(exception);
            Assert.IsType<NpgsqlException>(exception);
        }
    }
}
