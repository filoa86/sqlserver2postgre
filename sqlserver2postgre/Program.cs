using System;
using System.Data.SqlClient;
using System.Linq;
using sqlserver2postgre.Configuration;
using sqlserver2postgre.Utils;
using Microsoft.SqlServer.Types;

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

            if(sqlServer.State == System.Data.ConnectionState.Open &&
               postgreSql.State == System.Data.ConnectionState.Open)
            {
                var schemas = Migration.Schemas();
                if(schemas != null && schemas.Count() > 0)
                {
                    var queries = Migration.BuildQuery(schemas);

                    var sqlCommand = sqlServer.CreateCommand();
                    sqlCommand.CommandText = queries[0].Source;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            var fields = reader.VisibleFieldCount;
                            for (int i = 0; i < fields; i++) // all rows
                            {
                                var dataType = reader.GetDataTypeName(i);
                                // TODO: add coverage for all types
                                switch (dataType)
                                {
                                    case "int":
                                        Console.WriteLine(dataType);
                                        break;
                                    case "mssqlsource.sys.geometry":
                                        Console.WriteLine(dataType);
                                        break;
                                    default:
                                        throw new NotSupportedException("Please implement unsupported type: " + dataType);                                        
                                }                                
                            }
                            //SqlGeometry g = SqlGeometry.Deserialize(reader.GetSqlBytes(1));
                            //Console.WriteLine(String.Format("{0}, {1}, {2}", reader["id"].ToString(), g.ToString(), reader.GetType()));
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }

            // Avoid automatic exit 
            Console.WriteLine(string.Empty);
            Console.WriteLine("## Press ENTER to close application");
            Console.ReadLine();
        }
    }
}

