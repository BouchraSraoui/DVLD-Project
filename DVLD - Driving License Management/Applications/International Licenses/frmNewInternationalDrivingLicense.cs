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
    public partial class frmNewInternationalDrivingLicense : Form
    {
        double _Fees = 0;
        int _LocalLicenseID = -1;
        public frmNewInternationalDrivingLicense(int LocalLicenseID = -1)
        {
            InitializeComponent();

            if(LocalLicenseID != -1)
                _LocalLicenseID = LocalLicenseID;

        }

        #region Private Methods
        private void _DisplayLicenseInfo(int licenseID)
        {
            if(_LocalLicenseID != -1)
                licenseID = _LocalLicenseID;
           
            ctrlDrivingLicenseDetails2.DisplayDetails(licenseID, LicenseType.LocalLicense);
            lblShowLocalLicenseID.Text = licenseID.ToString();
            btnIssue.Enabled = true;

            llblShowLicensesHistory.Enabled = true;
        }

        private void _DisplayApplicationInfo()
        {
            lblShowApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedBy.Text = clsSessionInfo.User.UserName;

            _Fees = clsApplicationType.GetApplicationInfoByIDType((int)enApplicationType.New_International_License).ApplicationTypeFees;
            fees.Text = _Fees.ToString();

            DateTime IssueDate = DateTime.Now;
            lblIssueDate.Text = IssueDate.ToString();
            lblShowExpiraionDate.Text = clsFormat.DateToShort(IssueDate.AddYears(1));
        }

        private bool validateNormalLicense_NotExpired(clsLicense LocalLicense)
        {
            string Normal_License = "Class 3 - Ordinary driving license";

            if(LocalLicense.LicenseClass.ClassName != Normal_License)
            {
                MessageBox.Show("The Local License is not a normal License enter again please",
                                 "License Class",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return false;
            }
            if(LocalLicense.IsExpired())
            {

                MessageBox.Show("This Local License is Expired, you cannot create an international license",
                                "Expired License",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool saveInternationalLicense(clsInternationalLicense InternationalLicense)
        {
            if (InternationalLicense.Save())
            {
                MessageBox.Show("Data Saved Successfully");
                lblI_L_LicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                return true;
            }

            MessageBox.Show("Faild");
            return false;

        }

       
        private void _IssueInternationalLicense()
        {

            clsLicense LocalLicense = ctrlDrivingLicenseDetails2.License;

            if (LocalLicense.HasAlreadyActiveInterLicense())
            {
                MessageBox.Show("Internationl Created From This Local License is already active");
                return;
            }

            if (!validateNormalLicense_NotExpired(LocalLicense))
                return;

            clsInternationalLicense InternationalLicense = new clsInternationalLicense(LocalLicense, clsSessionInfo.User);

            //InternationalLicense.ExpirationDate = DateTime.Parse(lblShowExpiraionDate.Text);

            if (saveInternationalLicense(InternationalLicense))
                llblShowLicenseInfo.Enabled = true;

        }

        #endregion

        #region Events
        private void frmNewInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterByLicenseID1.FilterByLicenseID += _DisplayLicenseInfo;

            if (_LocalLicenseID != -1)
            {
                ctrlFilterByLicenseID1.ShowLicenseID = _LocalLicenseID;
                ctrlFilterByLicenseID1.Enabled = false;
            }

            _DisplayApplicationInfo();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _IssueInternationalLicense();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int InternationalLicenseID = Convert.ToInt32(lblI_L_LicenseID.Text);

            frm_License_Details frm_License_Details = new frm_License_Details(LicenseID: InternationalLicenseID , 
                                                                              LicenseType.InternationalLicense);
            frm_License_Details.ShowDialog();
        }

        private void llblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(ctrlDrivingLicenseDetails2.License.Driver);
            frmLicenseHistory.ShowDialog();
        }

        #endregion
    }
}
