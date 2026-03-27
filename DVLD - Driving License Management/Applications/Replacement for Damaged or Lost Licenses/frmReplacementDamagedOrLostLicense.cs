using BusinessLayerDVLD.enums;
using BusinessLayerDVLD;
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

namespace DVLD___Driving_License_Management.Applications.Replacement_for_Damaged_or_Lost_Licenses
{
    public partial class frmReplacementDamagedOrLostLicense : Form
    {
        clsApplicationType _ApplicationType = null;
        clsLicense _NewLicense = null;
        public frmReplacementDamagedOrLostLicense()
        {
            InitializeComponent();
        }

        #region Private Methods


        clsPerson _Person = null;

        /*private string _ShowStatus(enApplicationStatus Status)
        {
            switch (Status)
            {
                case enApplicationStatus.New:
                    return "New";
                case enApplicationStatus.Cancelled:
                    return "Cancelled";
                case enApplicationStatus.Completed:
                    return "Completed";
                default:
                    return "";
            }
        }
        public void DisplayApplicationDetails(clsApplication Application)
        {
            if (Application == null)
                return;

            lblShowID.Text = Application.ApplicationID.ToString();
            lblShowDate.Text = clsFormat.DateToShort(Application.ApplicationDate);
            lblShowStatusDate.Text = clsFormat.DateToShort(Application.LastStatusDate);

            lblShowFees.Text = Application.PaidFees.ToString();
            lblShowStatus.Text = _ShowStatus(Application.ApplicationStatus);
            lblShowType.Text = Application.ApplicationType.ApplicationTypeTitle;
            lblShowUser.Text = Application.CreatedByUser.UserName;

            if (Application.Person != null)
            {
                lblShowApplicant.Text = Application.Person.FullName;
                _Person = Application.Person;
            }
        }*/
        private void _SelectApplicationType()
        {
            enApplicationType ApplicationType;
            if (rdbtnDamagedLicense.Checked)
            {
                lblShow_Title.Text = "Replacement For Damaged License";
                ApplicationType = enApplicationType.Replacement_for_Damaged_Driving_License;
            }
            else
            {
                lblShow_Title.Text = "Replacement For Lost License";
                ApplicationType = enApplicationType.Replacement_for_Lost_Driving_License;
            }

            _ApplicationType = clsApplicationType.GetApplicationInfoByIDType((int)ApplicationType);
            lblShow_ApplicationFees.Text = _ApplicationType.ApplicationTypeFees.ToString();
        }

        private clsApplication _CreaeNewApplication(clsApplication PreviousApplication)
        {
            clsApplication NewApplication = new clsApplication(_ApplicationType,
                                                              PreviousApplication.Person,
                                                              clsSessionInfo.User);

            NewApplication.ApplicationStatus = enApplicationStatus.Completed;

            if (NewApplication.Save())
            {
                lblShow_L_R_Application.Text = NewApplication.ApplicationID.ToString();
                return NewApplication;
            }
            return null;
        }

        private clsLicense _BuildNewLicenseFrom(clsLicense OldLicense, clsApplication NewApplication)
        {
            clsLicense License = new clsLicense(NewApplication,
                                                OldLicense.Driver,
                                                OldLicense.LicenseClass,
                                                clsSessionInfo.User);

            License.ExpirationDate = License.IssueDate.AddYears(License.LicenseClass.DefaultValidityLength);
            License.Notes = OldLicense.Notes;

            _NewLicense = License;
            return License;
        }

        private void _OnLicenseSavedSuccessfully(clsLicense License)
        {
            MessageBox.Show("Data Saved Successfully");
            lblShow_ReplacedLicenseID.Text = License.LicenseID.ToString();
        }

        private bool _CreateReplacementLicense(clsLicense OldLicense)
        {
            clsApplication NewApplication = _CreaeNewApplication(OldLicense.Application);
            if (NewApplication == null)
                return false;

            clsLicense License = _BuildNewLicenseFrom(OldLicense, NewApplication);

            if (License.Save())
            {
                _OnLicenseSavedSuccessfully(License);
                return true;
            }

            return false;

        }

        private void _ReplacementInfo()
        {
            clsLicense OldLicense = ctrlDrivingLicenseDetails3.License;

            if (!OldLicense.ISActive)
            {
                MessageBox.Show("This License not active enter another license");
                return;
            }
            if (OldLicense.Disactive())
            {
                _CreateReplacementLicense(OldLicense);
            }

        }
        private void _DisplayLicenseInfo(int licenseID)
        {

            ctrlDrivingLicenseDetails3.DisplayDetails(licenseID,
                                                      LicenseType.LocalLicense);
            lblShow_OldLicenseID.Text = licenseID.ToString();
            btn_IssueReplacement.Enabled = true;
        }
        private void _InitializeControlswithValues()
        {
            rdbtnDamagedLicense.Checked = true;
            lblShow_ApplicationDate.Text = DateTime.Now.ToString();
            lblShow_CreatedBy.Text = clsSessionInfo.User.UserName;
        }
        #endregion


        #region Events
        private void frmReplacementDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterByLicenseID2.FilterByLicenseID += _DisplayLicenseInfo;

            _InitializeControlswithValues();

            _SelectApplicationType();
        }

        private void llbl_ShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_NewLicense.Driver);
            frmLicenseHistory.ShowDialog();
        }

        private void llbl_ShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_License_Details frm_License_Details = new frm_License_Details(LicenseID: _NewLicense.LicenseID,
                                                                               LicenseType.LocalLicense);
            frm_License_Details.ShowDialog();

        }

        private void btn_IssueReplacement_Click(object sender, EventArgs e)
        {
            _ReplacementInfo();
        }

        private void rdbtnDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _SelectApplicationType();
        }

        private void gbReplacementFor_Enter(object sender, EventArgs e)
        {
            _SelectApplicationType();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
