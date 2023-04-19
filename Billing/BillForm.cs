using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Billing
{
    public partial class BillForm : Form
    {
        public int id = 0;
        SelectForm select = new SelectForm();
        string connectionString = "SERVER= localhost;DATABASE= billing_system;USER= root;PASSWORD= root;";
        int num = 0;

        public BillForm()
        {
            InitializeComponent();
        }


        private void btnPlace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustName.Text))
            {
                MessageBox.Show("Please select a customer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query;
            string codes = "";

            int index = dgvBilling.RowCount;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM orders WHERE CustomerID = {id}";
                var cmd = new MySqlCommand(query, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count != 0)
                {
                    query = $"SELECT OrderID FROM orders WHERE CustomerID = {id}";
                    DataTable dt = Queries.ExecuteQuery(query);
                    int orderid = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        orderid = Convert.ToInt32(dr["OrderID"]);
                    }
                    query = $"SELECT COUNT(*) FROM orders WHERE OrderID = {orderid}";
                    cmd = new MySqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(count != 0)
                    {
                        DialogResult confirm = MessageBox.Show($"An order has already been placed for {txtCustName.Text}.\nWould you like to update order?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (confirm == DialogResult.Yes)
                        {

                            if (index == 0)
                            {
                                MessageBox.Show("You must choose at least one product to place an order.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            int length = dgvBilling.Rows.Count;

                            string[] codeArr = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                codeArr[i] = dgvBilling.Rows[i].Cells[0].Value.ToString();
                                codes = string.Join(",", codeArr);
                            }

                            double total = Convert.ToDouble(txtPrice.Text);

                            query = $"UPDATE orders SET Products= '{codes}', Total= '{total}' WHERE CustomerID = {id}";
                            Queries.ExecuteQuery(query);

                            MessageBox.Show("Your order has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        MessageBox.Show("You must choose at least one product to place an order.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int length = dgvBilling.Rows.Count;

                    string[] codeArr = new string[length];
                    for (int i = 0; i < length; i++)
                    {
                        codeArr[i] = dgvBilling.Rows[i].Cells[0].Value.ToString();
                        codes = string.Join(",", codeArr);
                    }

                    double total = Convert.ToDouble(txtPrice.Text);
                    query = $"INSERT INTO orders(CustomerID, Products, Total) VALUES( '{id}', '{codes}', '{total}')";
                    Queries.ExecuteQuery(query);

                    MessageBox.Show("Your order has been placed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dgv_display();
                chkif_OrderisPlaced();
            }
        }



        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgvBilling.SelectedRows[0].Index;
                int id = Convert.ToInt32(dgvBilling.Rows[index].Cells[0].Value);

                DialogResult del = MessageBox.Show("Would you like to remove this product from bill?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (del == DialogResult.Yes)
                {
                    string query = $"DELETE FROM prod_sel WHERE ID = {id}";
                    Queries.ExecuteNonQuery(query);

                    MessageBox.Show("Item has been removed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv_display();
                }
                else
                    MessageBox.Show("Item has been kept.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void btnSelectCust_Click(object sender, EventArgs e)
        {
            select.ShowDialog();
            this.Close();
        }
        


        private void btnNew_Click(object sender, EventArgs e)
        {
            id = 0;
            txtCustName.Text = "";
            txtCustCode.Text = "";
            boxProducts.SelectedItem = null;
            numQty.Value = 0;
            txtPrice.Text = "";
            dgvBilling.Columns.Clear();
        }



        private void btnIssue_Click(object sender, EventArgs e)
        {
            int rowCount = dgvBilling.Rows.Count;
            if (rowCount == 0) return;

            if(num != 0)
            {
                string query = $"SELECT OrderID FROM orders WHERE CustomerID = {id}";
                DataTable dt = Queries.ExecuteQuery(query);
                int orderid = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    orderid = Convert.ToInt32(dr["OrderID"]);
                }

                ReportForm form = new ReportForm();
                form.id = orderid;
                form.ShowDialog();
                this.Close();
            }
            else
            {
                DialogResult info = MessageBox.Show("You have not placed an order yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to exit?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                MessageBox.Show("GoodBye!");
                Application.Exit();
            }
        }


        private void BillForm_Load(object sender, EventArgs e)
        {
            boxProducts.Items.Clear();
            string create_tbl;

            create_tbl = "CREATE TABLE IF NOT EXISTS prod_sel(" +
                "ID int NOT NULL AUTO_INCREMENT, " +
                "Product varchar(50) NOT NULL, " +
                "Qty int NOT NULL, " +
                "Price decimal(18, 2) NOT NULL, " +
                "SubTotal decimal(18, 2) NOT NULL, " +
                "CustomerID int NOT NULL, " +
                "Primary KEY(ID), " +
                "Foreign KEY(CustomerID) REFERENCES customers(CustomerID) )";
            Queries.ExecuteNonQuery(create_tbl);


            create_tbl = "CREATE TABLE IF NOT EXISTS orders(" +
                "OrderID int NOT NULL AUTO_INCREMENT, " +
                "CustomerID int NOT NULL, " +
                "Products varchar(50) NOT NULL, " +
                "Total decimal(18,2) NOT NULL, " +
                "PRIMARY KEY(OrderID), " +
                "FOREIGN KEY(CustomerID) REFERENCES customers(CustomerID)  )";
            Queries.ExecuteNonQuery(create_tbl);
            

            string query = "SELECT * FROM user_logged";
            DataTable tbl = Queries.ExecuteQuery(query);
            foreach (DataRow dr in tbl.Rows)
            {
                txtServer.Text = dr["Username"].ToString(); 
            }

            query = "SELECT Name FROM products";
            tbl = Queries.ExecuteQuery(query);
            foreach(DataRow dr in tbl.Rows)
            {
                boxProducts.Items.Add(dr["Name"].ToString());
            }

            Select_User();
            chkif_OrderisPlaced();
            DelIfOrderNotPlaced();
            dgv_display();
        }



        private void Select_User()
        {
            string query = $"SELECT * FROM customers WHERE CustomerID = {id}";
            DataTable tbl = Queries.ExecuteQuery(query);
            foreach (DataRow dr in tbl.Rows)
            {
                txtCustName.Text = dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString();
                txtCustCode.Text = dr["CustomerID"].ToString();
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            string product = "";
            int qty = (int)numQty.Value;

            if (boxProducts.SelectedIndex > -1) product = boxProducts.SelectedItem.ToString();

            string query;
            DataTable dt;
            double price = 0;
            double subtotal = 0;

            try
            {
                if (string.IsNullOrEmpty(txtCustName.Text))
                {
                    MessageBox.Show("Please select a customer before picking products.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    Product prod = new Product(product, qty);
                    query = $"SELECT Price from products WHERE Name = '{prod._name}'";
                    dt = Queries.ExecuteQuery(query);
                    foreach (DataRow dr in dt.Rows)
                    {
                        price = Convert.ToDouble(dr["Price"]);
                    }
                    subtotal = price * prod._qty;

                    query = $"INSERT INTO prod_sel(Product, Qty, Price, SubTotal, CustomerID) VALUES('{prod._name}', '{prod._qty}', '{price}', '{subtotal}', {txtCustCode.Text})";
                    Queries.ExecuteNonQuery(query);

                    subtotal = 0;
                    price = 0;
                    dgv_display();
                    numQty.Value = 0;
                    boxProducts.SelectedIndex = -1;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void dgv_display()
        {
            string query;

            query = $"SELECT ID AS Code, Product, Price, Qty AS Quantity, SubTotal FROM prod_sel WHERE CustomerID = {id}";
            dgvBilling.DataSource = Queries.ExecuteQuery(query);

            double total, sub, tax;
            sub = 0;
            tax = 0.07;
            foreach (DataGridViewRow r in dgvBilling.Rows)
            {
                sub += Convert.ToDouble(r.Cells[4].Value);
            }
            total = sub + (sub * tax);

            txtPrice.Text = string.Format("{0:0.00}", total);
        }



        private void chkif_OrderisPlaced()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT COUNT(*) FROM orders WHERE CustomerID = {id}";
                var cmd = new MySqlCommand(query, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count != 0)
                {
                    query = $"SELECT OrderID FROM orders WHERE CustomerID = {id}";
                    DataTable dt = Queries.ExecuteQuery(query);
                    int orderid = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        orderid = Convert.ToInt32(dr["OrderID"]);
                    }
                    query = $"SELECT COUNT(*) FROM orders WHERE OrderID = {orderid}";
                    cmd = new MySqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(count != 0)
                        num = count;
                    conn.Close();
                }
            }
        }


        private void DelIfOrderNotPlaced()
        {
            string query;
            string products = "";

            if (id != 0)
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    query = $"SELECT COUNT(*) FROM prod_sel WHERE CustomerID = {id}";
                    var cmd = new MySqlCommand(query, conn);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(count != 0)
                    {
                        query = $"SELECT * FROM orders WHERE CustomerID = {id}";
                        DataTable dt = Queries.ExecuteQuery(query);

                        foreach (DataRow dr in dt.Rows)
                        {
                            products = dr["Products"].ToString();
                        }

                        string[] all_products = products.Split(',');
                        int length = all_products.Length;
                        int ind = Convert.ToInt32(all_products[length - 1]);

                        query = $"DELETE FROM prod_sel WHERE ID > {ind}";
                        Queries.ExecuteNonQuery(query);
                        conn.Close();   
                    }
                }
            }
        }
    }
}
