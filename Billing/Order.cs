using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    class Order
    {
        private string server;
        private int code;

        public Order(string server, int code)
        {
            this.server = server;
            this.code = code;
        }

        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public void Generate_Report()
        {
            string date = DateTime.Now.ToString();

        }

    }
}
