using BusinessLayerDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD___Driving_License_Management.Users
{
    public partial class frmUserDetails : Form
    {
        clsUser _User = null;
        public frmUserDetails(int userID)
        {
            InitializeComponent();

            _User = clsUser.GetUserByID(userID);
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            ctrlShowPersonInfo1.FillControls(_User.Person);
            ctrlUserInfo1.FillControls(_User);
        }
    }
}
