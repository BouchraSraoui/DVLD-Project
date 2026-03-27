namespace DVLD___Driving_License_Management.Applications
{
    partial class frmAddNewAppointmentcs
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlScheduleTest1 = new DVLD___Driving_License_Management.Controls.ctrlScheduleTest();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblShowTotalFees = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblShowRTestAppID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblShowAppFees = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(363, 753);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 48);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlScheduleTest1
            // 
            this.ctrlScheduleTest1.Location = new System.Drawing.Point(29, 12);
            this.ctrlScheduleTest1.Name = "ctrlScheduleTest1";
            this.ctrlScheduleTest1.Size = new System.Drawing.Size(541, 568);
            this.ctrlScheduleTest1.TabIndex = 31;
            this.ctrlScheduleTest1.Load += new System.EventHandler(this.ctrlScheduleTest1_Load);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(481, 753);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 48);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblShowTotalFees);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblShowRTestAppID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblShowAppFees);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 607);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(541, 140);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Retake Test Info:";
            // 
            // lblShowTotalFees
            // 
            this.lblShowTotalFees.AutoSize = true;
            this.lblShowTotalFees.Location = new System.Drawing.Point(451, 53);
            this.lblShowTotalFees.Name = "lblShowTotalFees";
            this.lblShowTotalFees.Size = new System.Drawing.Size(0, 22);
            this.lblShowTotalFees.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Total Fees:";
            // 
            // lblShowRTestAppID
            // 
            this.lblShowRTestAppID.AutoSize = true;
            this.lblShowRTestAppID.Location = new System.Drawing.Point(165, 104);
            this.lblShowRTestAppID.Name = "lblShowRTestAppID";
            this.lblShowRTestAppID.Size = new System.Drawing.Size(40, 22);
            this.lblShowRTestAppID.TabIndex = 3;
            this.lblShowRTestAppID.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "R.Test.App.ID:";
            // 
            // lblShowAppFees
            // 
            this.lblShowAppFees.AutoSize = true;
            this.lblShowAppFees.Location = new System.Drawing.Point(165, 53);
            this.lblShowAppFees.Name = "lblShowAppFees";
            this.lblShowAppFees.Size = new System.Drawing.Size(0, 22);
            this.lblShowAppFees.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "R.App.Fees:";
            // 
            // frmAddNewAppointmentcs
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(598, 813);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ctrlScheduleTest1);
            this.Controls.Add(this.btnClose);
            this.Name = "frmAddNewAppointmentcs";
            this.Text = "frmAddNewAppointmentcs";
            this.Load += new System.EventHandler(this.frmAddNewAppointmentcs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private DVLD___Driving_License_Management.Controls.ctrlScheduleTest ctrlScheduleTest1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblShowTotalFees;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblShowRTestAppID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblShowAppFees;
        private System.Windows.Forms.Label label1;
    }
}