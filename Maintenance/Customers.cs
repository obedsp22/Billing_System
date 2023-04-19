using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maintenance
{
    class Customers
    {
        private string fname;
        private string lname;

        public Customers(string fname, string lname)
        {
            this.fname = fname;
            this.lname = lname;
        }

        public string _fname
        {
            get 
            {
                if (Regex.IsMatch(this.fname, @"^[a-zA-Z'\s]"))
                    return fname;
                else if (string.IsNullOrEmpty(this.fname))
                    throw new Exception("Please enter first name.");
                else
                    throw new Exception("Name is in wrong format.");
            }
            set { fname = value; }
        }

        public string _lname
        {
            get 
            {
                if (Regex.IsMatch(this.lname, @"^[a-zA-Z'\s]"))
                    return lname;
                else if (string.IsNullOrEmpty(this.lname))
                    throw new Exception("Please enter last name.");
                else
                    throw new Exception("Name is in wrong format.");
            }
            set { lname = value; }
        }
    }
}
