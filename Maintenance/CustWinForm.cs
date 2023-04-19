using Maintenance;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchWindow
{
    public partial class CustWinForm : Base.SearchWinForm
    {
        int index = 0;
        public CustWinForm()
        {
            InitializeComponent();
        }


        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            base.btnSearch_Click(sender, e);

            string customer = Search();

            if (customer != null)
            {
                string query = $"SELECT * FROM customers WHERE First_Name LIKE '{customer}%' OR Last_Name LIKE '{customer}%'";
                GetData(Queries.ExecuteQuery(query));
            }
            else
                MessageBox.Show("You must enter customer name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        

        protected override void btnSelect_Click(object sender, EventArgs e)
        {
            base.btnSelect_Click(sender, e);

            index = Index();
            CustomerMaintenanceForm cust = new CustomerMaintenanceForm();
            cust.id = ID();
            cust.ShowDialog();
            this.Close();
        }


        protected override void btnPrint_Click(object sender, EventArgs e)
        {
            base.btnPrint_Click(sender, e);
            if(index >= 0)
            {
                printPreviewDialog1.Document = printCustomerInfo;
                printPreviewDialog1.ShowDialog();
            }
        }


        private void printCustomerInfo_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            index = Index();
            string fname = "";
            string lname = "";
            SelectedCustomer(ref fname, ref lname, index);

            e.Graphics.DrawString("Customer Information", new Font("Arial", 20 , FontStyle.Bold), Brushes.Black, new Point(280, 15));
            e.Graphics.DrawString($"First Name: ", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(100, 80));
            e.Graphics.DrawString(fname, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(230, 80));
            e.Graphics.DrawString($"Last Name: ", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(450, 80));
            e.Graphics.DrawString(lname, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(580, 80));
        }
    }
}
