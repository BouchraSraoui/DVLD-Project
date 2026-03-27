using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Controls;
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
    public partial class frmReniewDrivingLicense : Form
    {
        clsApplicationType _ApplicationType = null;
        double _Fees = 0;
        clsLicense _OldLicense = null;
        public frmReniewDrivingLicense()
        {
            InitializeComponent();
            _ApplicationType = clsApplicationType.GetApplicationInfoByIDType((int)enApplicationType.Renew_Driving_License_Service);
        }

        #region private methods

       
        
        private void _DisplayLicenseInfo(int licenseID)
        {

            ctrlDrivingLicenseDetails2.DisplayDetails(licenseID, LicenseType.LocalLicense);
            lblShowOldLicenseID.Text = licenseID.ToString();
           _OldLicense = ctrlDrivingLicenseDetails2.License;
            lblShowLicenseFees.Text = _OldLicense.PaidFees.ToString();

            lblShowTotalFees.Text = (double.Parse(lblShowAppFees.Text) + _OldLicense.PaidFees).ToString();


            btnReniew.Enabled = llblShowLicenseHictory.Enabled = true;
        }

        private clsApplication _CreaeNewApplication()
        {
            clsApplication NewApplication = new clsApplication(_ApplicationType,_OldLicense.Application.Person, clsSessionInfo.User);
            NewApplication.ApplicationStatus = enApplicationStatus.Completed;

            if (NewApplication.Save())
            {
                lblShowR_L_AppID.Text = NewApplication.ApplicationID.ToString();
                return NewApplication;
            }

            return null;
        }

        private void _DisplayApplicationInfo()
        {
            lblShowAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUserID.Text = clsSessionInfo.User.UserName;

            _Fees = _ApplicationType.ApplicationTypeFees;
            lblShowAppFees.Text = _Fees.ToString();

            DateTime IssueDate = DateTime.Now;
            lblShowIssueDate.Text = clsFormat.DateToShort(IssueDate);
            lblShowAppDate.Text = clsFormat.DateToShort(IssueDate);

        }

        private clsLicense _BuildNewLicense(clsApplication NewApplication)
        {
            clsLicense License = new clsLicense(NewApplication, _OldLicense.Driver,  _OldLicense.LicenseClass, clsSessionInfo.User);
            License.Notes = txtNotes.Text;
            License.PaidFees = _OldLicense.PaidFees+ NewApplication.PaidFees;
            License.ExpirationDate = _OldLicense.ExpirationDate;
          
            return License;

        }
        private void _ReniewLicense()
        {
            if (!_OldLicense.ISActive)
            {
                MessageBox.Show($"This license is inactive");
                return;
            }
               
            
            if (!_OldLicense.IsExpired())
            {
                MessageBox.Show($"Impossible ,this license will expired in {_OldLicense.ExpirationDate}"); 
                return;
            }
            
            clsApplication NewApplication = _CreaeNewApplication();
            if (NewApplication == null)
                return;
           
            clsLicense ReniewedLicense = _BuildNewLicense(NewApplication);
            if(ReniewedLicense.Save())
            {
                MessageBox.Show("Data Saved Successfully");

                lblShowReniewedLicenseID.Text = ReniewedLicense.LicenseID.ToString();
                llblShowNewLicenseInfo.Enabled = true;
            }

        }
        #endregion


        #region events
        private void frmReniewDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterByLicenseID2.FilterByLicenseID += _DisplayLicenseInfo;

            _DisplayApplicationInfo();
        }

        private void llblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = Convert.ToInt32(lblShowReniewedLicenseID.Text);

            frm_License_Details frm_License_Details = new frm_License_Details(LicenseID: LicenseID,
                                                                             LicenseType.LocalLicense);
            frm_License_Details.ShowDialog();
        }

        private void llblShowLicenseHictory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(ctrlDrivingLicenseDetails2.License.Driver);
           frmLicenseHistory.ShowDialog();
        }


        #endregion

        private void btnReniew_Click(object sender, EventArgs e)
        {
            _ReniewLicense();
        }

        private void ctrlDrivingLicenseDetails2_Load(object sender, EventArgs e)
        {

        }
    }
}
