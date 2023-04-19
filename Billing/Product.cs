using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    class Product
    {
        private string name;
        private int qty;

        public Product(string name, int qty)
        {
            this.name = name;
            this.qty = qty;
        }

        public string _name
        {
            get 
            {
                if(string.IsNullOrEmpty(name))
                    throw new Exception("Please choose a product.");
                else
                    return name;
            }
            set { name = value; }
        }

        public int _qty
        {
            get
            {
                if (qty > 0) return qty;
                else
                    throw new Exception("Quantity must be greater than 0.");
            }
            set { qty = value; }
        }
    }
}
