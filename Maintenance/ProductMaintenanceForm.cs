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
    public partial class ProductMaintenanceForm : Base.BaseForm_2
    {
        ProdWinForm prod = new ProdWinForm();
        public int id = 0;
        string name = "";

        public ProductMaintenanceForm()
        {
            InitializeComponent();
        }


        private void Display_DGV()
        {
            string query = $"SELECT * FROM products WHERE ProductID = {id}";
            DataTable tbl = Queries.ExecuteQuery(query);

            foreach(DataRow dr in tbl.Rows)
            {
                name = dr["Name"].ToString();
                txtProdID.Text = dr["ProductID"].ToString();
                txtProdName.Text = dr["Name"].ToString();
                txtProdPrice.Text = dr["Price"].ToString();
            }
        }


        private void ProductMaintenanceForm_Load(object sender, EventArgs e)
        {
            string query = $"CREATE TABLE IF NOT EXISTS products(" +
                 $"ProductID int NOT NULL AUTO_INCREMENT, " +
                 $"Name varchar(50) NOT NULL, " +
                 $"Price decimal(18,2) NOT NULL, " +
                 $"PRIMARY KEY(ProductID) )";
            Queries.ExecuteNonQuery(query);
            Display_DGV();
        }


        protected override void btnQuery_Click(object sender, EventArgs e)
        {
            base.btnQuery_Click(sender, e);
            prod.ShowDialog();
            this.Close();
        }


        protected override void btnSave_Click(object sender, EventArgs e)
        {
            base.btnSave_Click(sender, e);

            try
            {
                string name = txtProdName.Text;
                double price = Convert.ToDouble(txtProdPrice.Text);

                Products newProd = new Products(name, price);

                string query = "";
                string msg = "";

                if(string.IsNullOrEmpty(txtProdID.Text))
                {
                    query = $"INSERT INTO products(Name, Price) VALUES ('{newProd._name}', '{newProd.Price}')";
                    msg = "New product has been added.";
                }
                else
                {
                    DialogResult upd = MessageBox.Show("Do you want to update product information?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    
                    if(upd == DialogResult.Yes)
                    {
                        query = $"UPDATE products SET Name = '{newProd._name}', Price = '{newProd.Price}' WHERE ProductID = {id}";
                        msg = "Product has been updated.";
                    }
                    else
                    {
                        btnNew_Click(sender, e);
                        MessageBox.Show("Product has been saved. No changes made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (id > 0)
            {
                DialogResult del = MessageBox.Show($"Do you want to remove {name} from products", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (del == DialogResult.Yes)
                {
                    string query = $"DELETE FROM products WHERE ProductID = {id}";
                    Queries.ExecuteNonQuery(query);

                    MessageBox.Show("Product has been removed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNew_Click(sender, e);
                }
                else
                    MessageBox.Show("Product has been saved. No changes made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Choose a product from Query to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 


        protected override void btnNew_Click(object sender, EventArgs e)
        {
            base.btnNew_Click(sender, e);

            id = 0;
            txtProdID.Text = "";
            txtProdName.Text = "";
            txtProdPrice.Text = "";
        }
    }
}
