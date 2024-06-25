using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkAPI.App_Code
{
    public static class RegistrationHelper
    {
        public static string ConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["RegistrationDB"].ConnectionString; }
        }
    }
}