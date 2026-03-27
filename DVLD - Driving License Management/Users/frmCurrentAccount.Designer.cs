namespace DVLD___Driving_License_Management.Users
{
    partial class frmCurrentAccount
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
            this.ctrlShowPersonInfo1 = new DVLD___Driving_License_Management.Controls.ctrlShowPersonInfo();
            this.ctrlUserInfo1 = new DVLD___Driving_License_Management.Controls.ctrlUserInfo();
            this.SuspendLayout();
            // 
            // ctrlShowPersonInfo1
            // 
            this.ctrlShowPersonInfo1.Location = new System.Drawing.Point(101, 12);
            this.ctrlShowPersonInfo1.Name = "ctrlShowPersonInfo1";
            this.ctrlShowPersonInfo1.Size = new System.Drawing.Size(1203, 467);
            this.ctrlShowPersonInfo1.TabIndex = 0;
            this.ctrlShowPersonInfo1.Load += new System.EventHandler(this.ctrlShowPersonInfo1_Load);
            // 
            // ctrlUserInfo1
            // 
            this.ctrlUserInfo1.Location = new System.Drawing.Point(185, 509);
            this.ctrlUserInfo1.Name = "ctrlUserInfo1";
            this.ctrlUserInfo1.Size = new System.Drawing.Size(1014, 159);
            this.ctrlUserInfo1.TabIndex = 1;
            // 
            // frmCurrentAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1405, 695);
            this.Controls.Add(this.ctrlUserInfo1);
            this.Controls.Add(this.ctrlShowPersonInfo1);
            this.Name = "frmCurrentAccount";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlShowPersonInfo ctrlShowPersonInfo1;
        private Controls.ctrlUserInfo ctrlUserInfo1;
    }
}