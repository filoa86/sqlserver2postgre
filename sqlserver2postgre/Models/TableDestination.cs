using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Models
{
    public class TableDestination : Table
    {
        [JsonProperty("truncateBeforeInsert")]
        public bool TruncateBeforeInsert { get; set; }
        [JsonProperty("geometrySRID")] 
        public string GeometrySRID { get; set; }
    }
}
