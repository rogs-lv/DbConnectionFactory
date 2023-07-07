﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace DbConnectionFactory.Adapters
{
    /// <summary>
    /// MySqlAdapter class allow get connection to Postgres
    /// Assuming that connection string is right for Sql Server
    /// </summary>
    public class PostgreSqlAdapter : IAdapter
    {
		private readonly IConfiguration _configuration;
        private static DbConnection Connection { get; set; } = null;

        public PostgreSqlAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Get connection for Postgres Server
        /// </summary>
        /// <returns>IDbConnection</returns>
        /// <exception cref="SystemException">Error to connect server</exception>
        public IDbConnection GetConnection()
        {
            try
            {
                Connection.Open();
                return Connection;
            }
            catch (PostgresException ex)
            {
                throw ex;
            }
            catch (NpgsqlException ex) { 
                throw ex;
            }
        }
        /// <summary>
        /// Get Session
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetSession()
        {
            if (ConnectionState.Open != Connection.State)
                Connection.Open();
            
            return Connection;
        }
        /// <summary>
        /// Close connection
        /// </summary>
        public void CloseConnection()
        {
            if (ConnectionState.Open == Connection.State)
                Connection.Close();
        }
    }
}