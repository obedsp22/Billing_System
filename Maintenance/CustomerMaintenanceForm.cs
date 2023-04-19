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

namespace Maintenance
{
    public partial class CustomerMaintenanceForm : Base.BaseForm_2
    {
        CustWinForm cust = new CustWinForm();
        public int id = 0;
        string name = "";
        public CustomerMaintenanceForm()
        {
            InitializeComponent();
        }
        

        private void Dispay_DGV()
        {
            string query = $"SELECT * FROM customers WHERE CustomerID= {id}";
            DataTable tbl = Queries.ExecuteQuery(query);

            foreach(DataRow dr in tbl.Rows)
            {
                name = dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString();
                txtCustID.Text = dr["CustomerID"].ToString();
                txtCustFname.Text = dr["First_Name"].ToString();
                txtCustLname.Text = dr["Last_Name"].ToString();
            }
        }


        private void CustomerMaintenanceForm_Load(object sender, EventArgs e)
        {
            string query = $"CREATE TABLE IF NOT EXISTS customers(" +
                    $"CustomerID int NOT NULL AUTO_INCREMENT, " +
                    $"First_Name varchar(50) NOT NULL, " +
                    $"Last_Name varchar(50) NOT NULL, " +
                    $"PRIMARY KEY(CustomerID) )";
            Queries.ExecuteNonQuery(query);
            Dispay_DGV();
        }

        
        protected override void btnQuery_Click(object sender, EventArgs e)
        {
            base.btnQuery_Click(sender, e);
            cust.ShowDialog();
            this.Close();
        }


        protected override void btnSave_Click(object sender, EventArgs e)
        {
            base.btnSave_Click(sender, e);

            string fname, lname;

            fname = txtCustFname.Text;
            lname = txtCustLname.Text;

            try
            {
                Customers newCust = new Customers(fname, lname);

                string query = "";
                string msg = "";
                if (string.IsNullOrEmpty(txtCustID.Text))
                {
                    query = $"INSERT INTO customers(First_Name, Last_Name) VALUES('{newCust._fname}','{newCust._lname}')";
                    msg = "New Customer has been added.";
                }
                else
                {
                    DialogResult upd = MessageBox.Show($"Do you want to update customer info?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (upd == DialogResult.Yes)
                    {
                        query = $"UPDATE customers SET First_Name = '{newCust._fname}', Last_Name = '{newCust._lname}' WHERE CustomerID = {id}";
                        msg = "Customer has been updated.";
                    }
                    else
                    {
                        btnNew_Click(sender, e);
                        MessageBox.Show("Customer has been saved. No changes made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                Queries.ExecuteNonQuery(query);

                MessageBox.Show(msg, "Success", MessageBoxButtons.OK);

                btnNew_Click(sender, e);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        protected override void btnResolve_Click(object sender, EventArgs e)
        {
            base.btnResolve_Click(sender, e);

            if(id > 0)
            {
                DialogResult del = MessageBox.Show($"Do you want to remove {name} from customers", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                
                if(del == DialogResult.Yes)
                {
                    string query = $"DELETE FROM customers WHERE CustomerID = {id}";
                    Queries.ExecuteNonQuery(query);

                    MessageBox.Show("Customer has been removed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNew_Click(sender, e);
                }
                else
                    MessageBox.Show("Customer has been saved. No changes made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Choose a customer from Query to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        protected override void btnNew_Click(object sender, EventArgs e)
        {
            base.btnNew_Click(sender, e);
            id = 0;
            txtCustFname.Text = "";
            txtCustLname.Text = "";
            txtCustID.Text = "";
        }
    }
}
