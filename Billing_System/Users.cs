using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Billing_System
{
    class Users
    {
        private string _type;
        private string _user;
        private string _name;

        public Users(string type, string user, string pass)
        {
            this._type = type;
            this._user = user;
            this._name = pass;
        }

        public string type
        {
            get 
            {
                if (string.IsNullOrEmpty(this._type))
                    throw new Exception("You must choose an account type.");
                else
                    return _type;
            }
            set { _type = value; }
        }

        public string user
        {
            get 
            {
                if (string.IsNullOrEmpty(this._user))
                    throw new Exception("Please enter a username.");
                else
                    return _user;
            }
            set { _user = value; }
        }

        public string name
        {
            get 
            {
                if (string.IsNullOrEmpty(this._name))
                    throw new Exception("Please enter a administrator name.");
                else
                    return _name;
            }
            set { _name = value; }
        }
    }
}
