using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace sqlserver2postgre
{
    class Program
    {
        static void Main(string[] args)
        {
            var SQL_SERVER = ConfigurationManager.AppSettings["SQL_SERVER_ADDRESS"];
            var SQL_DATABASE = ConfigurationManager.AppSettings["SQL_SERVER_ADDRESS"];
            var SQL_USERID = ConfigurationManager.AppSettings["SQL_SERVER_ADDRESS"];
            var SQL_PASSWORD = ConfigurationManager.AppSettings["SQL_SERVER_ADDRESS"];
            // connection string sql server
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source=" + SQL_SERVER +
            ";Initial Catalog=" + SQL_DATABASE  +
            ";User id=UserName;" + SQL_USERID +
            ";Password=Secret" + SQL_PASSWORD + ";";
            //conn.Open();

            // connection string postgresql
        }
    }
}

