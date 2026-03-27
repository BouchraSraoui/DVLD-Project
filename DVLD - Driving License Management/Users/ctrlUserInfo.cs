using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;

namespace DVLD___Driving_License_Management.Controls
{
    public partial class ctrlUserInfo : UserControl
    {
       // clsUser _UserAccount;
        /*public ctrlUserInfo(clsUser UserAccount)
        {
            InitializeComponent();
            _UserAccount = UserAccount;
        }*/
        public ctrlUserInfo()
        {
            InitializeComponent();
        }
        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {

        }
        public void FillControls(clsUser User)
        {
            lblShowIsActive.Text = User.ISActive
                                           ? "Yes" 
                                           : 
                                           "No";
            lblShowUSerNAme.Text = User.UserName;
            lblShowUSrID.Text = User.UserID.ToString();
        }


    }
}
