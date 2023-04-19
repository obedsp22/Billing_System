using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maintenance
{
    class Products
    {
        private string name;
        private double price;

        public Products(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        public string _name
        {
            get 
            {
                if (string.IsNullOrEmpty(this.name))
                    throw new Exception("Please enter name for product.");
                else
                    return name;

            }
            set { name = value; }
        }

        public double Price
        {
            get 
            {
                if (price > 0) return price;
                else
                    throw new Exception("Price must be greater than 0.");
            }
            set { price = value; }
        }
    }
}
