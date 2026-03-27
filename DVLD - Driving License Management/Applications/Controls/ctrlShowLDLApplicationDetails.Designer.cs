namespace DVLD___Driving_License_Management.Controls
{
    partial class ctrlShowLDLApplicationDetails
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblShowPassedTests = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblShowD_L_Applicaion = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblShowLicenseClass = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ctrlBasicApplicationInfo1 = new DVLD___Driving_License_Management.Applications.Controls.ctrlBasicApplicationInfo();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.lblShowPassedTests);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.lblShowD_L_Applicaion);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.lblShowLicenseClass);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(28, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1238, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driving License Application Info";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(118, 111);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(189, 25);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Show License Info";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblShowPassedTests
            // 
            this.lblShowPassedTests.AutoSize = true;
            this.lblShowPassedTests.Location = new System.Drawing.Point(802, 111);
            this.lblShowPassedTests.Name = "lblShowPassedTests";
            this.lblShowPassedTests.Size = new System.Drawing.Size(60, 20);
            this.lblShowPassedTests.TabIndex = 17;
            this.lblShowPassedTests.Text = "label22";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(617, 109);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(138, 22);
            this.label23.TabIndex = 18;
            this.label23.Text = "Passed Tests:";
            // 
            // lblShowD_L_Applicaion
            // 
            this.lblShowD_L_Applicaion.AutoSize = true;
            this.lblShowD_L_Applicaion.Location = new System.Drawing.Point(233, 55);
            this.lblShowD_L_Applicaion.Name = "lblShowD_L_Applicaion";
            this.lblShowD_L_Applicaion.Size = new System.Drawing.Size(60, 20);
            this.lblShowD_L_Applicaion.TabIndex = 18;
            this.lblShowD_L_Applicaion.Text = "label19";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(550, 55);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(205, 22);
            this.label18.TabIndex = 17;
            this.label18.Text = "Appeced For License:";
            // 
            // lblShowLicenseClass
            // 
            this.lblShowLicenseClass.AutoSize = true;
            this.lblShowLicenseClass.Location = new System.Drawing.Point(802, 57);
            this.lblShowLicenseClass.Name = "lblShowLicenseClass";
            this.lblShowLicenseClass.Size = new System.Drawing.Size(60, 20);
            this.lblShowLicenseClass.TabIndex = 16;
            this.lblShowLicenseClass.Text = "label17";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "D_L_Applicaion:";
            // 
            // ctrlBasicApplicationInfo1
            // 
            this.ctrlBasicApplicationInfo1.Location = new System.Drawing.Point(4, 216);
            this.ctrlBasicApplicationInfo1.Name = "ctrlBasicApplicationInfo1";
            this.ctrlBasicApplicationInfo1.Size = new System.Drawing.Size(1262, 401);
            this.ctrlBasicApplicationInfo1.TabIndex = 1;
            // 
            // ctrlShowLDLApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlBasicApplicationInfo1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlShowLDLApplicationDetails";
            this.Size = new System.Drawing.Size(1288, 636);
            this.Load += new System.EventHandler(this.ctrlShowLDLApplicationDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblShowD_L_Applicaion;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblShowLicenseClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblShowPassedTests;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Applications.Controls.ctrlBasicApplicationInfo ctrlBasicApplicationInfo1;
    }
}
