using Maintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchWindow
{
    public partial class ProdWinForm : Base.SearchWinForm
    {
        int index = 0;
        public ProdWinForm()
        {
            InitializeComponent();
        }


        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            base.btnSearch_Click(sender, e);

            string product = Search();

            if (product != null)
            {
                string query = $"SELECT * FROM products WHERE Name LIKE '{product}%' ";
                GetData(Queries.ExecuteQuery(query));
            }
            else
                MessageBox.Show("You must enter a product name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        protected override void btnSelect_Click(object sender, EventArgs e)
        {
            base.btnSelect_Click(sender, e);

            index = Index();
            ProductMaintenanceForm prod = new ProductMaintenanceForm();
            prod.id = ID();
            prod.ShowDialog();
            this.Close();
        }


        protected override void btnPrint_Click(object sender, EventArgs e)
        {
            base.btnPrint_Click(sender, e);

            if (index >= 0)
            {
                printPreviewDialog1.Document = printproductinfo;
                printPreviewDialog1.ShowDialog();
            }
        }


        private void printproductinfo_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            index = Index();
            string pname = "";
            double price = 0.00D;
            SelectedProduct(ref pname, ref price, index);
            string customer = "Prodcut Info\nProduct name: " + pname + "\nPrice: $" + price;


            e.Graphics.DrawString("Product Information", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(280, 15));
            e.Graphics.DrawString("Product Name: ", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(100, 80));
            e.Graphics.DrawString(pname, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(230, 80));
            e.Graphics.DrawString("Product Price", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(450, 80));
            e.Graphics.DrawString($"${price}", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(580, 80));
        }
    }
}
