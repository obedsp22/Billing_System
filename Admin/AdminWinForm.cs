using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class AdminWinForm : Base.BaseForm
    {
        public AdminWinForm()
        {
            InitializeComponent();
        }

        private void AdminWinForm_Load(object sender, EventArgs e)
        {
            BaseForm f = new BaseForm();
            f.UserType("Admin");
        }
    }
}
