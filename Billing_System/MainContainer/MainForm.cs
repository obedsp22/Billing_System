using Billing;
using Maintenance;
using SearchWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainContainer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductMaintenanceForm prod = new ProductMaintenanceForm();
            prod.ShowDialog();
        }

        private void customersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CustomerMaintenanceForm cust = new CustomerMaintenanceForm();
            cust.ShowDialog();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProdWinForm form = new ProdWinForm();
            form.ShowDialog();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustWinForm form = new CustWinForm();
            form.ShowDialog();
        }

        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BillForm bill = new BillForm();
            bill.ShowDialog();
        }
    }
}
