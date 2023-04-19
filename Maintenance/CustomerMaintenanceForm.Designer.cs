namespace Maintenance
{
    partial class CustomerMaintenanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.txtCustFname = new System.Windows.Forms.TextBox();
            this.txtCustLname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Customer ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Customer First Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Customer Last Name";
            // 
            // txtCustID
            // 
            this.txtCustID.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.txtCustID.Location = new System.Drawing.Point(215, 121);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.ReadOnly = true;
            this.txtCustID.Size = new System.Drawing.Size(181, 20);
            this.txtCustID.TabIndex = 8;
            // 
            // txtCustFname
            // 
            this.txtCustFname.Location = new System.Drawing.Point(215, 195);
            this.txtCustFname.Name = "txtCustFname";
            this.txtCustFname.Size = new System.Drawing.Size(181, 20);
            this.txtCustFname.TabIndex = 9;
            // 
            // txtCustLname
            // 
            this.txtCustLname.Location = new System.Drawing.Point(215, 270);
            this.txtCustLname.Name = "txtCustLname";
            this.txtCustLname.Size = new System.Drawing.Size(181, 20);
            this.txtCustLname.TabIndex = 10;
            // 
            // CustomerMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCustLname);
            this.Controls.Add(this.txtCustFname);
            this.Controls.Add(this.txtCustID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CustomerMaintenanceForm";
            this.Text = "Customer Maintenance";
            this.Load += new System.EventHandler(this.CustomerMaintenanceForm_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtCustID, 0);
            this.Controls.SetChildIndex(this.txtCustFname, 0);
            this.Controls.SetChildIndex(this.txtCustLname, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.TextBox txtCustFname;
        private System.Windows.Forms.TextBox txtCustLname;
    }
}