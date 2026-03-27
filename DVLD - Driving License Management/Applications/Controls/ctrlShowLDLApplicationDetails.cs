using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Applications;
using DVLD___Driving_License_Management.Global_Classes;
using DVLD___Driving_License_Management.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD___Driving_License_Management.Controls
{
    public partial class ctrlShowLDLApplicationDetails : UserControl
    {
        clsLocalDrivingLicenseApplication _LDLApplication = null;
        int _LicenseID;
        public string PassedTests
        {
            set
            {
                lblShowPassedTests.Text = value;
            }
        }

        public ctrlShowLDLApplicationDetails()
        {
            InitializeComponent();
        }

        // public delegate void ShowPassedTests(int PassedTests);
        private void _ResetLocalDrivingLicenseApplicationInfo()
        {

            ctrlBasicApplicationInfo1.ResetApplicationInfo();
            lblShowD_L_Applicaion.Text = "[????]";
           // lblAppliedFor.Text = "[????]";

        }


        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            lblShowD_L_Applicaion.Text = _LDLApplication.LocalDrivingLicenseApplicationID.ToString();
            lblShowLicenseClass.Text = _LDLApplication.LicenseClass.ClassName;
            lblShowPassedTests.Text = _LDLApplication.PassedTests.ToString() + "/3";

            ctrlBasicApplicationInfo1.DisplayApplicationDetails(_LDLApplication.Application);
        }
        public void ShowLDLApplicationDetails(clsLocalDrivingLicenseApplication  LDLApplication)
        {
            if (LDLApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application provided!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LDLApplication = LDLApplication;


            _LicenseID = clsLicense.GetLicenseIDByAPPId(_LDLApplication.ApplicationID);
            linkLabel1.Enabled = _LicenseID != -1;

            _FillLocalDrivingLicenseApplicationInfo();

        }

        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_License_Details frm_License_Details = new frm_License_Details(_LicenseID, LicenseType.LocalLicense);
            frm_License_Details.ShowDialog();
        }

        private void ctrlShowLDLApplicationDetails_Load(object sender, EventArgs e)
        {


        }
    }
}
