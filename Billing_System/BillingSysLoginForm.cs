using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Billing_System
{
    public partial class BillingSysLoginForm : Form
    {
        string connection = "SERVER= localhost; USER= root; PASSWORD= root;";
        string connectionString = "SERVER= localhost;DATABASE= billing_system;USER= root;PASSWORD= root;";

        public BillingSysLoginForm()
        {
            InitializeComponent();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username, password;
             
            username = txtUser.Text;
            password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RegForm reg = new RegForm();
            AdForm admin = new AdForm();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT COUNT(username) FROM user_accounts WHERE username= '{username}'";
                var cmd = new MySqlCommand(query, conn);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if(count > 0)
                {
                    query = $"SELECT COUNT(password) FROM user_accounts WHERE username= '{username}' AND password= '{password}'";
                    cmd = new MySqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        query = $"SELECT COUNT(userType) FROM user_accounts WHERE username= '{username}' AND password= '{password}' AND userType = 'Administration'";
                        cmd= new MySqlCommand(query, conn);
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            admin.ID = GetUserId(username, password);
                            LoginUser(username, "Administration");
                            admin.ShowDialog();
                            Clear_Info();
                        }
                        else
                        {
                            reg.ID = GetUserId(username, password);
                            LoginUser(username, "Regular");
                            reg.ShowDialog();
                            Clear_Info();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password is incorrect.", "Invalid password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Username has not been registered in our systems.", "Invalid username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }


        private void BillingSysLogin_Load(object sender, EventArgs e)
        {
            try
            {
                string create_Db = "CREATE DATABASE IF NOT EXISTS billing_system";
                string create_Tbl = "CREATE TABLE IF NOT EXISTS user_accounts(" +
                        "userID int NOT NULL AUTO_INCREMENT, " +
                        "userType varchar(20) NOT NULL, " +
                        "username varchar(50) NOT NULL, " +
                        "password varchar(50) NOT NULL, " +
                        "name varchar(50), " +
                        "PRIMARY KEY(userID) )";
                string query = "CREATE TABLE IF NOT EXISTS user_logged(" +
                "Username varchar(50) NOT NULL, " +
                "Type varchar(50) NOT NULL)";

                Create_(connection, create_Db);
                Create_(connectionString, create_Tbl);
                Create_(connectionString, query);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void Create_(string connection, string create)
        {
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                var cmd = new MySqlCommand(create, conn);
                cmd.ExecuteNonQuery();
            }
        }


        private void checkBox__CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
                txtPassword.UseSystemPasswordChar = true;
        }


        private int GetUserId(string user, string password)
        {
            int id = 0;
            string query = $"SELECT * FROM user_accounts WHERE username = '{user}' AND password = '{password}'";
            DataTable dt = Queries.ExecuteQuery(query);

            foreach(DataRow row in dt.Rows)
            {
                id = Convert.ToInt32(row["userID"].ToString());
            }
            return id;
        }

        private void LoginUser(string username, string type)
        {
            string query;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                query = "SELECT COUNT(*) FROM user_logged";
                var cmd = new MySqlCommand(query, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if(count != 0) 
                {
                    query = "DELETE FROM user_logged";
                    Queries.ExecuteNonQuery(query);
                }
                query = $"INSERT INTO user_logged VALUES ('{username}', '{type}')";
                Queries.ExecuteNonQuery(query);
            }
        }

        private void Clear_Info()
        {
            txtPassword.Text = string.Empty;
            txtUser.Text = string.Empty;
        }
    }
}
