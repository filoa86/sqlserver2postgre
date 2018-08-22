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
                    var insertQuery = string.Empty;
                    foreach (var query in queries)
                    {
                        var rowToMigrate = 0;
                        var sqlCommand = sqlServer.CreateCommand();
                        sqlCommand.CommandText = query.Source;
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                rowToMigrate++;
                                var fields = reader.VisibleFieldCount;
                                var tmpQuery = query.Destination;
                                for (int i = 0; i < fields; i++) // all rows
                                {
                                    var dataType = reader.GetDataTypeName(i);
                                    // TODO: add coverage for all data types
                                    switch (dataType)
                                    {
                                        case "int":
                                            tmpQuery = tmpQuery.Replace("#" + reader.GetName(i), reader.GetInt32(i).ToString());
                                            break;
                                        case "mssqlsource.sys.geometry":
                                            var geom = SqlGeometry.Deserialize(reader.GetSqlBytes(i));
                                            tmpQuery = tmpQuery.Replace("#" + reader.GetName(i), "ST_GeomFromText('" + geom.ToString() + "')");
                                            break;
                                        default:
                                            throw new NotSupportedException("Please implement unsupported type: " + dataType);
                                    }
                                }
                                insertQuery += tmpQuery;
                            }
                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }

                        Console.Write("Executing data migration...");
                        try
                        {
                            var pgCommand = postgreSql.CreateCommand();
                            pgCommand.CommandText = insertQuery;
                            var affectedRows = pgCommand.ExecuteNonQuery();
                            if (affectedRows == rowToMigrate)
                                Console.WriteLine("SUCCESS: written " + affectedRows + " of " + rowToMigrate + " rows");
                            else
                                Console.WriteLine("ERROR: written " + affectedRows + " of " + rowToMigrate + " rows");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: " + e.Message);
                        }
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

