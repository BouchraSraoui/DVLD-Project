namespace DVLD___Driving_License_Management.Applications
{
    partial class frm_License_Details
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDrivingLicenseDetails1 = new DVLD___Driving_License_Management.Controls.ctrlDrivingLicenseDetails();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1059, 677);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 59);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(357, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 58);
            this.label1.TabIndex = 2;
            this.label1.Text = "Driving License Info";
            // 
            // ctrlDrivingLicenseDetails1
            // 
            this.ctrlDrivingLicenseDetails1.Location = new System.Drawing.Point(37, 117);
            this.ctrlDrivingLicenseDetails1.Name = "ctrlDrivingLicenseDetails1";
            this.ctrlDrivingLicenseDetails1.Size = new System.Drawing.Size(1125, 554);
            this.ctrlDrivingLicenseDetails1.TabIndex = 3;
            this.ctrlDrivingLicenseDetails1.Load += new System.EventHandler(this.ctrlDrivingLicenseDetails1_Load);
            // 
            // frm_License_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(1210, 744);
            this.Controls.Add(this.ctrlDrivingLicenseDetails1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_License_Details";
            this.Text = "frm_License_Details";
            this.Load += new System.EventHandler(this.frm_License_Details_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private Controls.ctrlDrivingLicenseDetails ctrlDrivingLicenseDetails1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private DVLD___Driving_License_Management.Controls.ctrlDrivingLicenseDetails ctrlDrivingLicenseDetails1;
    }
}