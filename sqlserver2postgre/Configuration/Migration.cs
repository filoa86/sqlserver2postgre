using Newtonsoft.Json;
using sqlserver2postgre.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Configuration
{
    public static class Migration
    {
        public static List<MigrationSchema> Schemas()
        {
            var config = new Configuration();
            var result = new List<MigrationSchema>();

            foreach (string file in Directory.EnumerateFiles(config.WorkDir, "*.json"))
            {
                var content = File.ReadAllText(file);
                var migrationSchema = JsonConvert.DeserializeObject<MigrationSchema>(content);
                result.Add(migrationSchema);
            }

            return result;
        }

        public static List<MigrationQuery> BuildQuery(List<MigrationSchema> schemas)
        {
            var result = new List<MigrationQuery>();
            foreach(var schema in schemas)
            {
                // source query
                var sourceQuery = string.Empty;
                sourceQuery = "SELECT " + string.Join(",", schema.Source.Columns) + " FROM " + schema.Source.Name;

                // destination
                var destinationQuery = string.Empty;
                destinationQuery = "INSERT INTO " + schema.Destination.Name + " (" + string.Join(",", schema.Destination.Columns) + ") " +
                                   "VALUES (#" + string.Join(",#", schema.Source.Columns) + "); ";

                // migration
                result.Add(new MigrationQuery()
                {
                    Source = sourceQuery,
                    Destination = destinationQuery
                });
            }
            return result;
        }
    }
}
