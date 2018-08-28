using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Models
{
    public class MigrationSchema
    {
        [JsonProperty("sourceTable")]
        public TableSource Source { get; set; }

        [JsonProperty("destinationTable")]
        public TableDestination Destination { get; set; }
    }
}
