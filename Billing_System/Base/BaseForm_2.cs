using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base
{
    public partial class BaseForm_2 : Form
    {
        public BaseForm_2()
        {
            InitializeComponent();
        }

        protected virtual void btnQuery_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnResolve_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnNew_Click(object sender, EventArgs e)
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
    }
}
