using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver2postgre.Configuration
{
    public class Configuration
    {
        public Configuration()
        {
            MsSqlServer = ConfigurationManager.AppSettings["MSSQL_SERVER_ADDRESS"];
            MsSqlDatabase = ConfigurationManager.AppSettings["MSSQL_DATABASE_NAME"];
            MsSqlUserId = ConfigurationManager.AppSettings["MSSQL_DATABASE_USERID"];
            MsSqlPassword = ConfigurationManager.AppSettings["MSSQL_DATABASE_PASSWORD"];

            PgSqlServer = ConfigurationManager.AppSettings["PG_SERVER_ADDRESS"];
            PgSqlDatabase = ConfigurationManager.AppSettings["PG_DATABASE_NAME"];
            PgSqlUserId = ConfigurationManager.AppSettings["PG_DATABASE_USERID"];
            PgSqlPassword = ConfigurationManager.AppSettings["PG_DATABASE_PASSWORD"];
            PgSqlPort = ConfigurationManager.AppSettings["PG_CONNECTION_PORT"];
        }

        public string MsSqlServer { get; private set; }
        public string MsSqlDatabase { get; private set; }
        public string MsSqlUserId { get; private set; }
        public string MsSqlPassword { get; private set; }

        public string PgSqlServer { get; private set; }
        public string PgSqlDatabase { get; private set; }
        public string PgSqlUserId { get; private set; }
        public string PgSqlPassword { get; private set; }
        public string PgSqlPort { get; private set; }
    }
}
