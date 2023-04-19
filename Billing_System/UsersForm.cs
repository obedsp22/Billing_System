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
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }


        private void UsersForm_Load(object sender, EventArgs e)
        {
            string query = "SELECT userType as 'Type', username as 'User', name as 'Name' FROM user_accounts";
            dgvUserAcc.DataSource = Queries.ExecuteQuery(query);
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string type, user, name, pass;

            user = txtUser.Text;
            name = txtName.Text;
            pass = txtPass.Text;

            if (boxAdmin.Checked == true) type = "Administration";
            else if (boxReg.Checked == true) type = "Regular";
            else type = "";

            try
            {
                Users newUser = new Users(type, user, name);
                string query = $"INSERT INTO user_accounts(userType, username, password, name) VALUES('{newUser.type}','{newUser.user}', '{pass}', '{newUser.name}')";
                Queries.ExecuteNonQuery(query);

                MessageBox.Show("New account has been added.", "Success", MessageBoxButtons.OK);

                UsersForm_Load(sender, e);
                Clear_Form();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnUpd_Click(object sender, EventArgs e)
        {
            string type, user, name, pass;

            user = txtUser.Text;
            name = txtName.Text;
            pass = txtPass.Text;

            if (boxAdmin.Checked == true) type = "Administration";
            else if (boxReg.Checked == true) type = "Regular";
            else type = "";

            try
            {
                Users newUser = new Users(type, user, name);

                DialogResult upd = MessageBox.Show("Would you like to update account?", "Are you sure??", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (upd == DialogResult.Yes) 
                {
                    string query = $"UPDATE user_accounts SET" +
                    $" userType= '{newUser.type}', username= '{newUser.user}', password= '{pass}', name= '{newUser.name}' WHERE userID= {ID}";
                    Queries.ExecuteNonQuery(query);

                    MessageBox.Show("Account has been updated.", "Success", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("Account has been saved.", "No changes made.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UsersForm_Load(sender, e);
                Clear_Form();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnRem_Click(object sender, EventArgs e)
        {
            DialogResult del = MessageBox.Show("Would you like to delete account?", "Are you sure??", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if(del == DialogResult.Yes)
            {
                string query = $"DELETE FROM user_accounts WHERE userID= {ID}";
                Queries.ExecuteNonQuery(query);
                MessageBox.Show("Account has been removed.", "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Account has been saved.", "No changes made.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UsersForm_Load(sender, e);
            Clear_Form();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        int ID = 0;
        private void dgvUserAcc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtUser.Text = dgvUserAcc.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvUserAcc.Rows[e.RowIndex].Cells[2].Value.ToString();
            string type = dgvUserAcc.Rows[e.RowIndex].Cells[0].Value.ToString();
            string user = dgvUserAcc.Rows[e.RowIndex].Cells[1].Value.ToString();
            string pass= "";

            if (type == "Administration")
                boxAdmin.Checked = true;
            else boxReg.Checked = true;

            string query = $"SELECT userID, password FROM user_accounts WHERE username = '{user}' AND userType = '{type}'";
            DataTable dt = Queries.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                ID= Convert.ToInt32(row["userID"]);
                pass = row["password"].ToString();
            }
            txtPass.Text = pass;
        }


        private void Clear_Form()
        {
            txtUser.Text= string.Empty;
            txtName.Text= string.Empty;
            txtPass.Text= string.Empty;
            boxAdmin.Checked = false;
            boxReg.Checked = false;
            ID = 0;
        }


        private void Show_()
        {
            if(boxAdmin.Checked == true)
            {
                lbltxt.Visible = true;
                txtName.Visible = true;
                lbltxt.Text = "Administrator";
            }
            if(boxReg.Checked == true)
            {
                lbltxt.Visible = true;
                txtName.Visible = true;
                lbltxt.Text = "Name";
            }
            txtPass.Visible = true;
            lblPass.Visible = true;
            checkBox1.Visible = true;
        }

        private void boxAdmin_CheckedChanged(object sender, EventArgs e)
        {
            Show_();
        }

        private void boxReg_CheckedChanged(object sender, EventArgs e)
        {
            Show_();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true) txtPass.UseSystemPasswordChar = false;
            else txtPass.UseSystemPasswordChar = true;
        }
    }
}
