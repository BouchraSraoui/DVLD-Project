using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
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
    public partial class frm_Issue_Driving_License_First_Time : Form
    {
        clsLocalDrivingLicenseApplication _LDLApp = null;

        public frm_Issue_Driving_License_First_Time(int LDLAppID)
        {
            InitializeComponent();

            _LDLApp=clsLocalDrivingLicenseApplication.GetLDLAppWithTestResult(LDLAppID);
        }


        #region Private Methods

        private bool _Create_saved_License(clsApplication Application, clsDriver Driver, clsLicenseType LicenseClass)
        {
            clsLicense License = new clsLicense(Application, Driver, LicenseClass, clsSessionInfo.User);
            License.Notes = textBox1.Text;

            if (License.Save())
            {
                MessageBox.Show("License With ID = " + License.LicenseID + "is Saved Successfulle");
                return true;
            }
            return false;
        }

        private void _SaveApplication(clsApplication Application)
        {
            Application.ApplicationStatus = enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.Save();
        }

        private void _IssueLicense()
        {
            clsApplication Application = _LDLApp.Application;
            clsDriver Driver = null;

            int DriverID = clsDriver.isDriver(Application.PersonID);

            if (DriverID == -1)
            {
                clsPerson Person = Application.Person;
                if (Person == null)
                    return;

                Driver = new clsDriver(Person, clsSessionInfo.User);
                Driver.Save();
            }
            else
                Driver=  clsDriver.GetDriver(DriverID);
              
            if (_Create_saved_License(Application, Driver  , _LDLApp.LicenseClass))
                _SaveApplication(Application);

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void frm_Issue_Driving_License_First_Time_Load(object sender, EventArgs e)
        {
            ctrlShowApplicationDetails1.ShowLDLApplicationDetails(_LDLApp);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _IssueLicense();

            btnIssue.Enabled = false;
        }

        private void ctrlShowApplicationDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
