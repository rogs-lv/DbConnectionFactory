using DbConnectionFactory.Adapters;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace DbConnectionFactoryTests.Adapters
{
    public class MySqlAdapterTest
    {
        private readonly MySqlAdapter _mySqlAdapter;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<MySqlConnection> _mockMySqlConnection;
        private readonly Mock<DbConnection> _connection;

        public MySqlAdapterTest()
        {
            _configuration = new();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")]).Returns("Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;");
            _configuration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);
            _mySqlAdapter = new (_configuration.Object);
            _connection = new ();
            _mockMySqlConnection = new("");
        }
        
        [Fact]
        public void GetConnection_ValidConfiguration_ReturnIDbConnection() {

            _mockMySqlConnection.Setup(s => s.Open()).Callback(() => _connection.Object);

            
            var result = _mySqlAdapter.GetConnection();

            Assert.IsAssignableFrom<IDbConnection>(result);
        }

        [Fact]
        public void GetConnection_ValidConfiguration_ReturnOpenConnection()
        {

            var result = _mySqlAdapter.GetConnection();

            Assert.Equal(ConnectionState.Open, result.State);
        }

        [Fact]
        public void GetConnection_InvalidConfiguration_ReturnException()
        {

            Exception exception = Assert.Throws<SystemException>(() => _mySqlAdapter.GetConnection());

        }
    }
}
