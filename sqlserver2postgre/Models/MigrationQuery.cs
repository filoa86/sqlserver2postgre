using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Models
{
    public class MigrationQuery : ICloneable
    {
        public string Source { get; set; }

        public Destination Destination { get; set; }

        public object Clone()
        {
            return new MigrationQuery()
            {
                Source = this.Source,
                Destination = (Destination)this.Destination.Clone()
            };
        }
    }

    public class Destination : ICloneable
    {
        public string SQL { get; set; }
        public string GeometrySRID { get; set; }

        public object Clone()
        {
            return new Destination()
            {
                SQL = this.SQL,
                GeometrySRID = this.GeometrySRID
            };
        }
    }
}
