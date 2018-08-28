using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Models
{
    public class TableSource : Table
    {
        [JsonProperty("recordLimit")]
        public int RecordLimit { get; set; }
    }
}
