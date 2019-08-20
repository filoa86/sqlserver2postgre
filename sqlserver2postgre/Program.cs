using System;
using System.Data.SqlClient;
using System.Linq;
using sqlserver2postgre.Configuration;
using sqlserver2postgre.Utils;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using sqlserver2postgre.Models;

namespace sqlserver2postgre
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.WriteStartup();
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;

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
                    var insertQuery = new List<string>();
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
                                var tmpQuery = (Destination)query.Destination.Clone();
                                for (int i = 0; i < fields; i++) // all rows
                                {
                                    var dataType = reader.GetDataTypeName(i);
                                    // TODO: add coverage for all data types
                                    if(dataType == "int")
                                    {
                                        tmpQuery.SQL = tmpQuery.SQL.Replace("#" + reader.GetName(i), reader.GetInt32(i).ToString());
                                    }
                                    else if (dataType.Contains("sys.geometry"))
                                    {
                                        var geom = SqlGeometry.Deserialize(reader.GetSqlBytes(i));
                                        tmpQuery.SQL = tmpQuery.SQL.Replace("#" + reader.GetName(i), string.Format("ST_GeomFromText('{0}',{1})", geom.ToString(), tmpQuery.GeometrySRID));
                                    }
                                    else
                                    {
                                        throw new NotSupportedException("Please implement unsupported type: " + dataType);
                                    }                                    
                                }
                                insertQuery.Add(tmpQuery.SQL);                                
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
                            var affectedRows = 0;
                            var pgCommand = postgreSql.CreateCommand();
                            var recNr = insertQuery.Count();
                            insertQuery.ForEach(qry =>
                            {
                                pgCommand.CommandText = qry;
                                affectedRows += pgCommand.ExecuteNonQuery();
                                Console.Write("\rMigrated rows: {0} of {1}", affectedRows, recNr);
                            });
                            
                            if (affectedRows == rowToMigrate)
                                Console.WriteLine("\nSUCCESS: written " + affectedRows + " of " + rowToMigrate + " rows");
                            else
                                Console.WriteLine("\nERROR: written " + affectedRows + " of " + rowToMigrate + " rows");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nERROR: " + e.Message);
                        }
                    }                                        
                }
            }

            endTime = DateTime.Now;
            TimeSpan span = endTime.Subtract(startTime);
            Console.WriteLine("Migration time (minutes): " + span.TotalMinutes);

            // Avoid automatic exit 
            Console.WriteLine("## Press ENTER to close application");
            Console.ReadLine();
        }
    }
}

