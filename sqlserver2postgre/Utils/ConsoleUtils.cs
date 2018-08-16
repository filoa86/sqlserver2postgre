using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteStartup()
        {
            System.Console.WriteLine("================================= SQL SERVER to POSTGRE SQL DATA MIGRATION TOOL =================================");
            // TODO: display connections
        }

        public static void WriteException(string context, string message)
        {
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("=================================================== EXCEPTION ===================================================");
            System.Console.WriteLine(context.ToUpper() + ": " + message);
            System.Console.WriteLine("=================================================================================================================");
        }        
    }
}
