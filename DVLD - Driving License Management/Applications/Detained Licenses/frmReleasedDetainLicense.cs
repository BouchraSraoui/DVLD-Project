using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Global_Classes;
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

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmReleasedDetainLicense : Form
    {
        clsLicense _License = null;

        clsDetainedLicense _detainedLicense = null;

        public frmReleasedDetainLicense(int LicenseID = -1)
        {
            InitializeComponent();

            if (LicenseID != -1)
                _initForm(LicenseID);

        }

        #region Methods

        private void _initForm(int licenseID)
        {
            ctrlFilterByLicenseID1.ShowLicenseID = licenseID;
            ctrlFilterByLicenseID1.Enabled = false;

            _DisplayLicenseInfo(licenseID);
        }

        private bool _ControlsEnabled()
        {
            bool condition = _License != null && _License.ISActive;

            llblShowLicenseInfo.Enabled = condition && !_License.isDeatined;
            llblShowLicenseHistory.Enabled = !(_License == null);

            return btnRelease.Enabled = condition && _License.isDeatined;
        }

        private void _DisplayLicenseInfo(int licenseID)
        {
            ctrlDrivingLicenseDetails1.DisplayDetails(licenseID, LicenseType.LocalLicense);

            lblShowLicenseID.Text = licenseID.ToString();

            _License = ctrlDrivingLicenseDetails1.License;
            _LicenseIsDetained();

            if (_ControlsEnabled())
                _showDetainLicenseInfo();
        }

        private bool _ShowDetainedLicenseInfo()
        {
            _detainedLicense = clsDetainedLicense.GetDetainLicenseInfo(_License.LicenseID);
            if (_detainedLicense == null)
                return false;

            lblShowDetainDate.Text = clsFormat.DateToShort(_detainedLicense.DetainDate);
            lblShowDetainID.Text = _detainedLicense.DetainID.ToString();
            lblShowCreatedBy.Text = _detainedLicense.CreatedByUser.UserName;
            lblShowFineFees.Text = _detainedLicense.FineFees.ToString();
            lblShowLicenseID.Text = _License.LicenseID.ToString();

            return true;
        }

        private void _showApplicationinfo(clsApplication ReleaseApplication)
        {
            lblShowApplicationFees.Text = ReleaseApplication.PaidFees.ToString();
            lblShowApplicationID.Text = ReleaseApplication.ApplicationID.ToString();
            lblShowTotalFees.Text = (ReleaseApplication.PaidFees + _detainedLicense.FineFees).ToString();
        }

        private clsApplication _Create_ShowNewApplicationInfo()
        {
            clsApplicationType ReleaseApplicationtype = clsApplicationType.GetApplicationInfoByIDType(
                                                        (int)enApplicationType.Release_Detained_Driving_Licsense
                                                                                              );

            clsApplication ReleaseApplication = new clsApplication(ReleaseApplicationtype,
                                                                   _License.Driver.Person, 
                                                                   clsSessionInfo.User);

            if (ReleaseApplication.Save())
            {
                _showApplicationinfo(ReleaseApplication);
                return ReleaseApplication;
            }
               
            return null;

        }

        private void _showDetainLicenseInfo()
        {
            if (!_License.isDeatined)
                return;

            if (!_ShowDetainedLicenseInfo())
            {
                MessageBox.Show("Faild To load detained license info",
                                "Faild" ,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

        }

        private void _ReleasedLicense(clsApplication ReleaseApplication)
        {
            _detainedLicense.ReleaseApplication = ReleaseApplication;
            _detainedLicense.ReleaseDate = DateTime.Now;
            _detainedLicense.IsReleased = true;

            if (_detainedLicense.save())
            {
                MessageBox.Show("Data saved successfully");

                _License.isDeatined = false;
                _ControlsEnabled();
            }
               
            else
                MessageBox.Show("Faild to save data");
        }
        private void _LicenseIsDetained()
        {
            if (_License.isDeatined)
                MessageBox.Show("License Is Detained",
                                 "Detained License");
        }
        private void _save()
        {
            if(_detainedLicense == null)
            {
                MessageBox.Show("Faild to upload detain license");
                return;
            }
            clsApplication ReleaseApplication = _Create_ShowNewApplicationInfo();

            if(ReleaseApplication == null)
            {
                MessageBox.Show("Faild to create a new Application");
                return;
            }

            _ReleasedLicense(ReleaseApplication);
        }

        #endregion
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleasedDetainLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterByLicenseID1.FilterByLicenseID += _DisplayLicenseInfo;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _save();
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
