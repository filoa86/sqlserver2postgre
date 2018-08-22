using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Models
{
    public class Table
    {
        [JsonProperty("tableName")]
        public string Name { get; set; }

        [JsonProperty("columns")]
        public string[] Columns { get; set; }
    }
}
