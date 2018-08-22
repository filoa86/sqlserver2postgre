using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Models
{
    public class MigrationQuery
    {
        public string Source { get; set; }

        public string Destination { get; set; }
    }
}
