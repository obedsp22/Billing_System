using MainContainer;
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
    public partial class AdForm : Base.BaseForm
    {
        public int ID = 0;
        public AdForm()
        {
            InitializeComponent();
        }

        private void AdForm_Load(object sender, EventArgs e)
        {
            string user = "Admin";
            UserType(user);
            Display_UserInfo();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            MainForm frm = new MainForm();
            frm.ShowDialog();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            UsersForm user = new UsersForm();
            user.ShowDialog();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            PassForm pass = new PassForm();
            pass.id= ID;
            pass.ShowDialog();
        }


        private void Display_UserInfo()
        {
            string admin = "";
            string username = "";
            string code = "";

            string query = $"SELECT * FROM user_accounts WHERE userID = {ID}";
            DataTable dt = Queries.ExecuteQuery(query);

            foreach (DataRow r in dt.Rows)
            {
                admin = r["name"].ToString();
                username = r["username"].ToString();
                code = "00" + r["userID"].ToString();
            }

            txtUsername.Text = username;
            txtCode.Text = code;
            txtUserType.Text = admin;
        }
    }
}
