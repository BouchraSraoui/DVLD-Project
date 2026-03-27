namespace DVLD___Driving_License_Management.Controls
{
    partial class ctrlUserInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblShowUSrID = new System.Windows.Forms.Label();
            this.lblShowUSerNAme = new System.Windows.Forms.Label();
            this.lblShowIsActive = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login Information";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(78, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "User ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(391, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(719, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Is Active:";
            // 
            // lblShowUSrID
            // 
            this.lblShowUSrID.AutoSize = true;
            this.lblShowUSrID.Location = new System.Drawing.Point(215, 110);
            this.lblShowUSrID.Name = "lblShowUSrID";
            this.lblShowUSrID.Size = new System.Drawing.Size(51, 20);
            this.lblShowUSrID.TabIndex = 4;
            this.lblShowUSrID.Text = "label5";
            // 
            // lblShowUSerNAme
            // 
            this.lblShowUSerNAme.AutoSize = true;
            this.lblShowUSerNAme.Location = new System.Drawing.Point(538, 114);
            this.lblShowUSerNAme.Name = "lblShowUSerNAme";
            this.lblShowUSerNAme.Size = new System.Drawing.Size(51, 20);
            this.lblShowUSerNAme.TabIndex = 6;
            this.lblShowUSerNAme.Text = "label7";
            // 
            // lblShowIsActive
            // 
            this.lblShowIsActive.AutoSize = true;
            this.lblShowIsActive.Location = new System.Drawing.Point(853, 114);
            this.lblShowIsActive.Name = "lblShowIsActive";
            this.lblShowIsActive.Size = new System.Drawing.Size(51, 20);
            this.lblShowIsActive.TabIndex = 7;
            this.lblShowIsActive.Text = "label8";
            // 
            // ctrlUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblShowIsActive);
            this.Controls.Add(this.lblShowUSerNAme);
            this.Controls.Add(this.lblShowUSrID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctrlUserInfo";
            this.Size = new System.Drawing.Size(1014, 166);
            this.Load += new System.EventHandler(this.ctrlUserInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblShowUSrID;
        private System.Windows.Forms.Label lblShowUSerNAme;
        private System.Windows.Forms.Label lblShowIsActive;
    }
}
