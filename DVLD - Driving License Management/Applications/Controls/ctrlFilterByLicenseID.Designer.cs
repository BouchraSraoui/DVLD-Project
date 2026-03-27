namespace DVLD___Driving_License_Management.Controls
{
    partial class ctrlFilterByLicenseID
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
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.txtPrintLicenseID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseID.Location = new System.Drawing.Point(20, 19);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(121, 25);
            this.lblLicenseID.TabIndex = 1;
            this.lblLicenseID.Text = "License ID:";
            this.lblLicenseID.Click += new System.EventHandler(this.lblLicenseID_Click);
            // 
            // txtPrintLicenseID
            // 
            this.txtPrintLicenseID.Location = new System.Drawing.Point(187, 18);
            this.txtPrintLicenseID.Name = "txtPrintLicenseID";
            this.txtPrintLicenseID.Size = new System.Drawing.Size(476, 26);
            this.txtPrintLicenseID.TabIndex = 3;
            this.txtPrintLicenseID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrintLicenseID_KeyDown_1);
            // 
            // ctrlFilterByLicenseID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLicenseID);
            this.Controls.Add(this.txtPrintLicenseID);
            this.Name = "ctrlFilterByLicenseID";
            this.Size = new System.Drawing.Size(682, 54);
            this.Load += new System.EventHandler(this.ctrlFilterByLicenseID_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.TextBox txtPrintLicenseID;
    }
}
