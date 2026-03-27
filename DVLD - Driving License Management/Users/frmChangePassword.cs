using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using BusinessLayerDVLD;
namespace DVLD___Driving_License_Management.Users
{
    
    public partial class Change_Password : Form
    {
        clsUser _User = clsSessionInfo.User;
        public Change_Password(clsUser User = null)
        {
            InitializeComponent();

            if (User != null)
                _User = User;
        }


        private bool _SetErrors(Control TextBox , string error)
        {
            errorProvider1.SetError(TextBox, error);
            return error == "";
        }

        private void _VisiblityOfControls(bool visible)
        {
            txtConfirmPassWord.Visible = visible;
            txtNewPassWord.Visible = visible;
            label1.Visible = visible;
            label3.Visible = visible;
            txtCurretPassWord.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void Change_Password_Load(object sender, EventArgs e)
        {

            _VisiblityOfControls(false);

            ctrlShowPersonInfo1.FillControls(_User.Person);

            ctrlUserInfo1.FillControls(_User);
                                       
        }

        private void txtCurretPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            string CurrentPassword = txtCurretPassWord.Text.Trim();
            string error = "";

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(CurrentPassword))
                {
                    error = "Empty";
                }
                 else
                {
                    if(CurrentPassword != _User.Password)
                    {
                        error = "Enter a Correct Password";
                    }
                }
                 


                if(_SetErrors(txtCurretPassWord, error))
                {
                    _VisiblityOfControls(true);
                    txtCurretPassWord.Enabled = false;
                   
                }
                   

                e.SuppressKeyPress = true;

            }
            
        }

        private void txtNewPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            string NewPassword = txtNewPassWord.Text.Trim();

            string error = "";
            if (e.KeyCode == Keys.Enter)
            {
               if(NewPassword == _User.Password)
               {
                    error = "Enter a New Password";  
               }

                if (!_SetErrors(txtNewPassWord, error))
                    txtNewPassWord.Focus();

                else
                    txtNewPassWord.Tag = NewPassword;



                e.SuppressKeyPress = true;

            }
        }

        private void txtConfirmPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            string ConfirmPassword = txtConfirmPassWord.Text.Trim();
            string error = "";

            

            if (e.KeyCode == Keys.Enter)
            {
                if (ConfirmPassword != txtNewPassWord.Text.Trim())
                {
                    error = "New And Confirm Password are differenets";
                }

                if (!_SetErrors(txtConfirmPassWord, error))
                    txtConfirmPassWord.Focus();

                else

                    _User.Password = txtNewPassWord.Text.Trim();

                e.SuppressKeyPress = true;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            /*if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            _User.Password = txtNewPassWord.Text;
            if (_User.Save())
            {
                btnSave.Enabled = false;
                MessageBox.Show("Password Changed Successfully.",
                 "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _VisiblityOfControls(false);
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
