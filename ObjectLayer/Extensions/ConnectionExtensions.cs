using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLayer.Extensions
{
    public static class ConnectionExtensions
    {
        public static string GetConnectionString()
        {
            return string.Format("server={0};port={1};user={2};password={3};database={4}",
                Environment.GetEnvironmentVariable("SQL_IP")
                , Environment.GetEnvironmentVariable("SQL_PORT")
                , Environment.GetEnvironmentVariable("SQL_USER")
                , Environment.GetEnvironmentVariable("SQL_PASWORD")
                , Environment.GetEnvironmentVariable("SQL_DB"));
        }
    }
}
