using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using sqlserver2postgre.Configuration;
using sqlserver2postgre.Utils;

namespace sqlserver2postgre
{
    public class Program
    {
        

        
        static void Main(string[] args)
        {
            ConsoleUtils.WriteStartup();

            // SQL Server
            var sqlServer = Connection.SqlConnection();

            // PostgeSQL
            var postgreSql = Connection.NpgsqlConnection();



            // Avoid automatic exit 
            Console.WriteLine(string.Empty);
            Console.WriteLine("## Press ENTER to close application");
            Console.ReadLine();
        }
    }
}

