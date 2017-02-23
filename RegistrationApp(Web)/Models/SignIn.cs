using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using University;

namespace RegistrationApp_Web_.Models
{
    public class SignIn
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public bool InfoCheck(string name, string pass)
        {
            return ConnectionObj.CheckUserInfo(name, pass);
        }
    }
}
