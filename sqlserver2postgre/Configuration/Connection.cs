using Npgsql;
using sqlserver2postgre.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Configuration
{
    public static class Connection
    {
        private const string _msSqlConnString = "Data Source={0};Initial Catalog={1};User id={2};Password={3};";
        private const string _pgSqlConnString = "Server={0};Port={1};User Id={2};Password={3};Database={4};";

        private static SqlConnection _sqlConnection = null;
        private static NpgsqlConnection _npgsqlConnection = null;

        public static SqlConnection SqlConnection()
        {
            try
            {
                if (_sqlConnection == null)
                {
                    var config = new Configuration();
                    var connectionString = String.Format(_msSqlConnString, config.MsSqlServer, config.MsSqlDatabase, config.MsSqlUserId, config.MsSqlPassword);
                    _sqlConnection = new SqlConnection(connectionString);
                }

                if (_sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    _sqlConnection.Open();
                    Console.WriteLine("SQL Server connection SUCCESS");
                }
                    
            }
            catch (Exception e)
            {
                ConsoleUtils.WriteException("SQL Server", e.Message);
            }
            return _sqlConnection;
        }

        public static NpgsqlConnection NpgsqlConnection()
        {
            try
            {
                if (_npgsqlConnection == null)
                {
                    var config = new Configuration();
                    var connectionString = String.Format(_pgSqlConnString, config.PgSqlServer, config.PgSqlPort, config.PgSqlUserId, config.PgSqlPassword, config.PgSqlDatabase);
                    _npgsqlConnection = new NpgsqlConnection(connectionString);
                }

                if (_npgsqlConnection.State != System.Data.ConnectionState.Open)
                {
                    _npgsqlConnection.Open();
                    Console.WriteLine("PostgreSQL connection SUCCESS");
                }
                   
            }
            catch (Exception e)
            {
                ConsoleUtils.WriteException("PostgreSQL", e.Message);
            }

            return _npgsqlConnection;
        }
    }
}
