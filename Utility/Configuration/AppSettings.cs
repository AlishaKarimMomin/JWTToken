using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Configuration
{
    public class AppSettings
    {
        private static IConfiguration _config;
        public static void InitializeConfig(IConfiguration config)
        {
            _config = config;
        }





        public static string SQL_Get_ConnectionString()
        {
            // Follow this pattern -> HOST_NAME:PORT_NUMBER,password=PASSWORD 
            string ConnectionString = _config["ConnectionStrings:DefaultConnection"];
            return ConnectionString;



        }



        public static DateTime JWT_TokenExpirationInSeconds()
        {
            // Follow this pattern -> HOST_NAME:PORT_NUMBER,password=PASSWORD 
            /*DateTime expiration = Convert.ToDateTime(_config["JWT:TokenExpirationInSeconds"]);
            return expiration;*/
            int seconds = int.Parse(_config["JWT:TokenExpirationInSeconds"]);
            DateTime currentDateTime = DateTime.Now;
            DateTime expiration = currentDateTime.AddSeconds(seconds);
            return expiration;



        }
    }
}