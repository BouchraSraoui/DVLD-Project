namespace DVLD___Driving_License_Management.Applications
{
    partial class frmScheduleTest
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
            this.components = new System.ComponentModel.Container();
            this.lblResults = new System.Windows.Forms.Label();
            this.rbtnPass = new System.Windows.Forms.RadioButton();
            this.rbtnFail = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ctrlScheduleTest1 = new DVLD___Driving_License_Management.Controls.ctrlScheduleTest();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(101, 621);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(90, 25);
            this.lblResults.TabIndex = 1;
            this.lblResults.Text = "Results:";
            // 
            // rbtnPass
            // 
            this.rbtnPass.AutoSize = true;
            this.rbtnPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPass.Location = new System.Drawing.Point(245, 622);
            this.rbtnPass.Name = "rbtnPass";
            this.rbtnPass.Size = new System.Drawing.Size(73, 24);
            this.rbtnPass.TabIndex = 2;
            this.rbtnPass.TabStop = true;
            this.rbtnPass.Text = "Pass";
            this.rbtnPass.UseVisualStyleBackColor = true;
            // 
            // rbtnFail
            // 
            this.rbtnFail.AutoSize = true;
            this.rbtnFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnFail.Location = new System.Drawing.Point(362, 624);
            this.rbtnFail.Name = "rbtnFail";
            this.rbtnFail.Size = new System.Drawing.Size(63, 24);
            this.rbtnFail.TabIndex = 3;
            this.rbtnFail.TabStop = true;
            this.rbtnFail.Text = "Fail";
            this.rbtnFail.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(115, 667);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Note:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(217, 667);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(608, 104);
            this.textBox1.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(606, 793);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 47);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(718, 793);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ctrlScheduleTest1
            // 
            this.ctrlScheduleTest1.Location = new System.Drawing.Point(150, 12);
            this.ctrlScheduleTest1.Name = "ctrlScheduleTest1";
            this.ctrlScheduleTest1.Size = new System.Drawing.Size(541, 591);
            this.ctrlScheduleTest1.TabIndex = 0;
            this.ctrlScheduleTest1.Load += new System.EventHandler(this.ctrlScheduleTest1_Load);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmScheduleTest
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(869, 861);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbtnFail);
            this.Controls.Add(this.rbtnPass);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.ctrlScheduleTest1);
            this.Name = "frmScheduleTest";
            this.Text = "frmTakeVisiontest";
            this.Load += new System.EventHandler(this.frmTakeVisiontest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DVLD___Driving_License_Management.Controls.ctrlScheduleTest ctrlScheduleTest1;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.RadioButton rbtnPass;
        private System.Windows.Forms.RadioButton rbtnFail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}