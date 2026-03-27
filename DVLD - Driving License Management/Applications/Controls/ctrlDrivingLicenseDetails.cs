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
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Global_Classes;


namespace DVLD___Driving_License_Management.Controls
{
    

    public partial class ctrlDrivingLicenseDetails : UserControl
    {
        enum enIssueReason
        {
            FirstTime = 1,
            Reniew = 2,
            LostReplacement=3,
            DamagedReplacement = 4
        }

        clsLicense _License = null;
        clsInternationalLicense _InternationalLicense = null;
        public clsLicense License
        {
            get
            {
                return _License;
            }
        }
        public ctrlDrivingLicenseDetails()
        {
            InitializeComponent();
        }

        private string _DisplayIssueReason(int issueReason)
        {
            switch ((enIssueReason)issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time"; 

                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";

                case enIssueReason.LostReplacement:
                    return "Lost Replacement";

                case enIssueReason.Reniew:
                    return "Reniew";
            }
            return "";

        }

        private void _VissibiltyOfSomeControls(bool Visible)
        {
            lblShowClass.Visible = Visible;
            lblShowIssueReason.Visible = Visible;
            lblShowNotes.Visible = Visible;
            lblClass.Visible = Visible;
            lblIssueReason.Visible = Visible;
            lblNotes.Visible = Visible;
        }

        private void _DisplayLicenseInfo(clsLicense License)
        {
            
            lblShowDriverID.Text = License.Driver.DriverID.ToString();
            lblShowExpirationDate.Text = clsFormat.DateToShort((_InternationalLicense == null ? 
                                                                License.ExpirationDate : 
                                                                _InternationalLicense.ExpirationDate));

            lblShowIsActive.Text = _InternationalLicense == null ? 
                                  (License.ISActive ? "Yes" : "No") : 
                                  (_InternationalLicense.ISActive ? "Yes" : "No");

            lblShowIsDetained.Text = License.isDeatined ? "Yes" : "No";

            lblShowIssueDate.Text = clsFormat.DateToShort((_InternationalLicense == null ?
                                                            License.IssueDate :
                                                            _InternationalLicense.IssueDate));
            lblShowLicenseID.Text = (_InternationalLicense?.InternationalLicenseID ?? License.LicenseID).ToString();

            _DisplayPersonInfo(License.Driver.Person);

            if (_InternationalLicense != null)
            {
                _VissibiltyOfSomeControls(false);
                return;
            }
            lblShowClass.Text = License.LicenseClass.ClassName;
            lblShowIssueReason.Text = _DisplayIssueReason(License.IssueReason);
            lblShowNotes.Text = string.IsNullOrEmpty(License.Notes) ? 
                                "No Notes" : 
                                License.Notes;
        }

        private void _DisplayPersonInfo(clsPerson Person)
        {
            lblShowDateOfBirth.Text = clsFormat.DateToShort(Person.DateOfBirth);
            lblShowGendor.Text = Person.Gendor;
            lblShowNationalNo.Text = Person.NationalNo;
            lblShowName.Text = Person.FullName;

            string ImagePath = Person.ImagePath;

            if (!string.IsNullOrEmpty(ImagePath))
                pictureBox1.Image = Image.FromFile(ImagePath);
        }
     
        public void DisplayDetails(int LicenseID, LicenseType LicenseType)
        {
            clsLicense License;

            if (LicenseType == LicenseType.InternationalLicense)
            {
                clsInternationalLicense InternationalLicense = clsInternationalLicense.GetInterLicense(LicenseID);

                _InternationalLicense = InternationalLicense;
                License = _InternationalLicense.LocalLicense;
            }

            else
            {
                License = clsLicense.GetLicense(LicenseID);
                if (License == null)
                    return;
            }
            _License = License;
            _DisplayLicenseInfo(License);
        }

        private void ctrlDrivingLicenseDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
