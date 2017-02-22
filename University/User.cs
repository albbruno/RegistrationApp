using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class User 
    {
        public string Username { get { return Username; } }

        public string Password { get { return mPassword; } }

        public string Email { get { return mEmail; } }

        protected string  mUsername;

        protected string mPassword;

        protected string mEmail;

        public User()
        {

        }

        public User(string username, string password, string email)
        {
            mUsername = username;
            mPassword = password;
            mEmail = email;
        }
    }
}
