using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_System
{
    public partial class PassForm : Form
    {
        public int id = 0;
        public PassForm()
        {
            InitializeComponent();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) txtPassword.UseSystemPasswordChar = false;
            else txtPassword.UseSystemPasswordChar = true;
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true) txtConfirmPass.UseSystemPasswordChar = false;
            else txtConfirmPass.UseSystemPasswordChar = true;
        }


        private void btnChg_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPass.Text;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Both fields must be filled in, in order to continue.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(password == confirmPassword)
            {
                string query = $"UPDATE user_accounts SET password = '{password}' WHERE userID= {id}";
                Queries.ExecuteNonQuery(query);
                MessageBox.Show("Password has been changed.", "Success", MessageBoxButtons.OK);
                this.Close();
            }
            else
                MessageBox.Show("Passwords do not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
