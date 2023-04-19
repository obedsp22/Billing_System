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
    public partial class SelectForm : Base.SearchWinForm
    {
        int index = 0;

        public SelectForm()
        {
            InitializeComponent();
        }

        private void SelectForm_Load(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM customers";
            GetData(Queries.ExecuteQuery(query));
        }

        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            base.btnSearch_Click(sender, e);

            string item = Search();
            string query;

            query = $"SELECT * FROM customers WHERE First_Name LIKE '{item}%' or Last_Name LIKE '{item}%'";
            GetData(Queries.ExecuteQuery(query));
        }


        protected override void btnSelect_Click(object sender, EventArgs e)
        {
            base.btnSelect_Click(sender, e);

            index = Index();
            BillForm bill = new BillForm();
            bill.id = ID();
            bill.ShowDialog();
            this.Close();
        }


        protected override void btnPrint_Click(object sender, EventArgs e)
        {
            base.btnPrint_Click(sender, e);

            printPreviewDialog1.Document = printSelectDoc;
            printPreviewDialog1.ShowDialog();
        }

        private void printSelectDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int _id = 0;
            index = Index();
            _id = ID();
            string fname = "";
            string lname = "";
            SelectedCustomer(ref fname, ref lname, index);
            string customer = "Customer Info\nCustomer ID: " + _id + "\nFirst name: " + fname + " Last name: " + lname;

            float x = 150.0F;
            float y = 150.0F;
            float width = 250.0F;
            float height = 100.0F;
            RectangleF drawRect = new RectangleF(x, y, width, height);

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            e.Graphics.DrawString(customer, new Font("Microsoft Sans Serif", 12), new SolidBrush(Color.Black), drawRect, drawFormat);
        }
    }
}
