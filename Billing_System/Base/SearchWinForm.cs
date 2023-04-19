using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base
{
    public partial class SearchWinForm : Form
    {
        public SearchWinForm()
        {
            InitializeComponent();
        }

        protected virtual void btnSelect_Click(object sender, EventArgs e)
        {

        }

        protected  virtual void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to exit?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                MessageBox.Show("GoodBye!");
                Application.Exit();
            }
        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {

        }




        public void GetData(DataTable tbl)
        {
            dgvQuery.DataSource = tbl;
        }

        public string Search()
        {
            return txtSearch.Text;
        }

        public int Index()
        {
            int index = 0;
            try
            {
                index = dgvQuery.SelectedRows[0].Index;
            }
            catch (Exception)
            {
                MessageBox.Show("You must choose an item before proceeding.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return index;
        }

        int _ID = 0;
        public int ID()
        {
            return _ID;
        }

        public void SelectedCustomer(ref string fname, ref string lname, int index)
        {
            try
            {
                fname = dgvQuery.Rows[index].Cells[1].Value.ToString();
                lname = dgvQuery.Rows[index].Cells[2].Value.ToString();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SelectedProduct(ref string name, ref double price, int index)
        {
            try
            {
                name = dgvQuery.Rows[index].Cells[1].Value.ToString();
                price = Convert.ToDouble(dgvQuery.Rows[index].Cells[2].Value.ToString());
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        protected virtual void dgvQuery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            _ID = Convert.ToInt32(dgvQuery.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
    }
}
