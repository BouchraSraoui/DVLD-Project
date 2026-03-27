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

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmLocalDrivingLicenseApplicationDetails : Form
    {
        clsLocalDrivingLicenseApplication _LDLApp = null;
        public frmLocalDrivingLicenseApplicationDetails(int LDLAppID)
        {
            InitializeComponent();
            _LDLApp = clsLocalDrivingLicenseApplication.FindLDApp(LDLAppID);

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlShowApplicationDetails1.ShowLDLApplicationDetails(_LDLApp);
        }

    }
}
