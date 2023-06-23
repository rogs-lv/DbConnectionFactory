using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbConnectionFactory.Adapters
{
    public class MySqlAdapater : IAdapter
    {
        private readonly IConfiguration _configuration;

        public MySqlAdapater(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IDbConnection GetConnection()
        {
            try
            {
                DbConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                connection.Open();
                return connection;
            }
            catch (SqlException)
            {
                //log
                throw new SystemException("Error to connect Db");
            }
        }
    }
}
