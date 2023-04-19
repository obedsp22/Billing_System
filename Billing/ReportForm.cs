using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing
{
    public partial class ReportForm : Form
    {
        public int id = 0; 
        public ReportForm()
        {
            InitializeComponent();
        }

        private void Display_Report()
        {
            int customerid = 0; 
            int orderid = 0;
            string name = "";
            string products = "";
            string date = DateTime.Now.ToString();
            double total = 0;

            string query = $"SELECT * FROM orders WHERE OrderID = {id}";
            DataTable dt = Queries.ExecuteQuery(query);

            foreach(DataRow dr in dt.Rows)
            {
                orderid = Convert.ToInt32(dr["OrderID"]);
                customerid = Convert.ToInt32(dr["CustomerID"]);
                products = dr["Products"].ToString();
                total = Convert.ToDouble(dr["Total"]);
            }

            string[] all_products = products.Split(',');
            int length = all_products.Length;

            int index = Convert.ToInt32(all_products[length - 1]);
            

            query = $"SELECT * FROM customers WHERE customerID = {customerid}";
            dt= Queries.ExecuteQuery(query);
            foreach(DataRow dr in dt.Rows)
            {
                name = dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString();
            }
            
            txtDate.Text = date;
            txtName.Text = name;
            txtCode.Text = customerid.ToString();
            txtOrderNum.Text = orderid.ToString();
            txtTotal.Text = string.Format("{0:0.00}", total);

            query = $"SELECT ID AS Code, Product, Price, Qty AS Quantity, SubTotal FROM prod_sel WHERE CustomerID = {customerid} AND ID <= {index}";
            dgvYourProd.DataSource= Queries.ExecuteQuery(query);
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            Display_Report();
        }


        Bitmap bitmap;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            this.Controls.Add(panel);

            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);

            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
