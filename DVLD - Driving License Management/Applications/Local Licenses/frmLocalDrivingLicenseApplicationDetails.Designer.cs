namespace DVLD___Driving_License_Management.Applications
{
    partial class frmLocalDrivingLicenseApplicationDetails
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
            this.ctrlShowApplicationDetails1 = new DVLD___Driving_License_Management.Controls.ctrlShowLDLApplicationDetails();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlShowApplicationDetails1
            // 
            this.ctrlShowApplicationDetails1.Location = new System.Drawing.Point(12, 21);
            this.ctrlShowApplicationDetails1.Name = "ctrlShowApplicationDetails1";
            this.ctrlShowApplicationDetails1.Size = new System.Drawing.Size(1288, 636);
            this.ctrlShowApplicationDetails1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1190, 676);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 46);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmLocalDrivingLicenseApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1336, 734);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlShowApplicationDetails1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLicenseApplicationDetails";
            this.Text = "frmLocalDrivingLicenseApplication";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplication_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DVLD___Driving_License_Management.Controls.ctrlShowLDLApplicationDetails ctrlShowApplicationDetails1;
        private System.Windows.Forms.Button btnClose;
    }
}