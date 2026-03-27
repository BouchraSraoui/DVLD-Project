using DVLD___Driving_License_Management.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Global_Classes;

namespace DVLD___Driving_License_Management
{
    public partial class frmLogin : Form
    {
        
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Methods
        private bool _AccountIsCorrect_Active()
        {
            clsUser User = clsUser.GetUser(txtShowUserName.Text,
                                           mtxtShowPassWord.Text);
            if(!User.ISActive)
            {
                lblShowError.Text = "Your account is not Active!";
                return false;
            }
            if (User == null )
            {
                lblShowError.Text = "Invalid Username/Password!";
                return false;

            }

            clsSessionInfo.User = User;

            return true;

        }
        private void _LoadUserInfo()
        {
            string username = "";
            string password = "";
            if(clsGlobal.ReadInfoFromFile(ref username,
                                       ref password))
            {
                txtShowUserName.Text = username;
                mtxtShowPassWord.Text = password;
                checkBox1.Checked = true;
            }
            else
                checkBox1.Checked = false;
        }
        private void _rememberUser()
        {
            if(!checkBox1.Checked)
            {
                txtShowUserName.Text = "";
                mtxtShowPassWord.Text = "";
            }
                clsGlobal.RememberUsernameAndPassword(txtShowUserName.Text,
                                                  mtxtShowPassWord.Text);
        }

        #endregion

        #region Events

        private void button1_Click(object sender, EventArgs e)
        {

            if (!_AccountIsCorrect_Active())
                return;

            _rememberUser();

            this.Hide();
            frmMainScreen frm = new frmMainScreen(this);
            frm.ShowDialog();

        }
        private void frmLogin_Load_1(object sender, EventArgs e)
        {
            _LoadUserInfo();
        }


        #endregion
    }
}
