using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Controls;
using DVLD___Driving_License_Management.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD___Driving_License_Management.Controls.ctrlDrivingLicenseDetails;

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmDetaindLicense : Form
    {
        clsLicense _License = null;
        public frmDetaindLicense()
        {
            InitializeComponent();
        }

        private void _DisplayLicenseInfo(int licenseID)
        {
            ctrlDrivingLicenseDetails1.DisplayDetails(licenseID, LicenseType.LocalLicense);
            lblShowLicenseID.Text = licenseID.ToString();

            _License = ctrlDrivingLicenseDetails1.License;
            _ControlsEnabled();
        }

        private void _InitForm()
        {
            lblShowDetainDate .Text = DateTime.Now.ToString();
            lblShowCreatedBy.Text = clsSessionInfo.User.UserName;

            _ControlsEnabled();
        }

        private void _ControlsEnabled()
        {
            bool condition = _License != null && _License.ISActive;

            btnSave.Enabled = condition && !_License.isDeatined;
            llblShowLicenseInfo.Enabled = condition && _License.isDeatined;
            llblShowLicenseHistory.Enabled = !(_License == null);
        }

        private void _Save()
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense(_License, clsSessionInfo.User);
            //hna nrmlm nzid set error provider 
            detainedLicense.FineFees = Convert.ToInt64(textBox1.Text);

            if(detainedLicense.save())
            {
                MessageBox.Show("Data saved Successfully");
                lblShowDetainID.Text = detainedLicense.DetainID.ToString();

                _License.isDeatined = true;

                _ControlsEnabled();

            }
            else
                MessageBox.Show("Faild");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDetaindLicense_Load(object sender, EventArgs e)
        {
            _InitForm();

            ctrlFilterByLicenseID1.FilterByLicenseID += _DisplayLicenseInfo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_License_Details frm_License_Details = new frm_License_Details(_License.LicenseID, LicenseType.LocalLicense);
            frm_License_Details.ShowDialog();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_License.Driver);
            frmLicenseHistory.ShowDialog();
        }
    }
}
