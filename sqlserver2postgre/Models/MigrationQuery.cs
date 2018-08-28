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

        public Destination Destination { get; set; }
        
    }

    public class Destination
    {
        public string SQL { get; set; }
        public string GeometrySRID { get; set; }
    }
}
