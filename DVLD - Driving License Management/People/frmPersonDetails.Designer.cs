namespace DVLD___Driving_License_Management.Persons
{
    partial class frmPersonDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlShowPersonInfo1 = new DVLD___Driving_License_Management.Controls.ctrlShowPersonInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1072, 618);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 43);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(420, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 58);
            this.label1.TabIndex = 3;
            this.label1.Text = "Person Details";
            // 
            // ctrlShowPersonInfo1
            // 
            this.ctrlShowPersonInfo1.Location = new System.Drawing.Point(12, 140);
            this.ctrlShowPersonInfo1.Name = "ctrlShowPersonInfo1";
            this.ctrlShowPersonInfo1.Size = new System.Drawing.Size(1203, 521);
            this.ctrlShowPersonInfo1.TabIndex = 2;
            this.ctrlShowPersonInfo1.Load += new System.EventHandler(this.ctrlShowPersonInfo1_Load);
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 673);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlShowPersonInfo1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPersonDetails";
            this.Text = "frmPersonDetails";
            this.Load += new System.EventHandler(this.frmPersonDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       // private Controls.ctrlPersonDetails ctrlPersonDetails1;
        private System.Windows.Forms.Button btnClose;
        private Controls.ctrlShowPersonInfo ctrlShowPersonInfo1;
        private System.Windows.Forms.Label label1;
    }
}