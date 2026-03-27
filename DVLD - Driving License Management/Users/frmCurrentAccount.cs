using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;
namespace DVLD___Driving_License_Management.Users
{
    public partial class frmCurrentAccount : Form
    {
       // clsUser _User;
        public frmCurrentAccount()
        {
            InitializeComponent();
            
        }
        private void ctrlShowPersonInfo1_Load(object sender, EventArgs e)
        {
           


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            clsUser User = clsSessionInfo.User;

            ctrlShowPersonInfo1.FillControls(User.Person);

            ctrlUserInfo1.FillControls(User);
            
        }
    }
}
