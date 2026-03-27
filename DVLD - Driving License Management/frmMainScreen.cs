using DVLD___Driving_License_Management.Persons;
using DVLD___Driving_License_Management.Users;
using DVLD___Driving_License_Management.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DVLD___Driving_License_Management.Drivers;
using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Applications.Replacement_for_Damaged_or_Lost_Licenses;
using DVLD___Driving_License_Management.Applications.InternationalLicenses;

namespace DVLD___Driving_License_Management
{
    public partial class frmMainScreen : Form
    {
        frmLogin _frmlogin;
        public frmMainScreen(frmLogin frm)
        {
            InitializeComponent();
            _frmlogin = frm;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListPeople frmListPeople = new ListPeople();

            frmListPeople.ShowDialog();
        }

       
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmManageUSers frmManageUSers = new frmManageUSers();
            frmManageUSers.ShowDialog();
        }

        private void currentAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCurrentAccount currentAccount = new frmCurrentAccount();
            currentAccount.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
          Change_Password change_Password = new Change_Password();
          change_Password.ShowDialog();
        }
        
        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsSessionInfo.User = null;
            _frmlogin.Show();
            this.Close();  
        }

        private void releaseDetainedLicencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicense frmReleasedDetainLicense = new frmReleasedDetainLicense();
            frmReleasedDetainLicense.ShowDialog();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frnNewLDL frmnewDrivingLicense = new frnNewLDL(-1);
            frmnewDrivingLicense.ShowDialog();

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmManageApplications = new frmManageLocalDrivingLicenseApplications();
            frmManageApplications.ShowDialog();
        }

        private void drivingLicencesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void remplacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmReplacementDamagedOrLostLicense frmReplacementDamagedOrLostLicense = new frmReplacementDamagedOrLostLicense();
           frmReplacementDamagedOrLostLicense.ShowDialog();
        }

        private void internationslLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalDrivingLicense frmNewInternationalDrivingLicense= new frmNewInternationalDrivingLicense();
            frmNewInternationalDrivingLicense.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reniewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReniewDrivingLicense frmReniewDrivingLicense = new frmReniewDrivingLicense();
            frmReniewDrivingLicense.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDrivers frmManageDrivers = new frmManageDrivers();
            frmManageDrivers.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmManageApplicationtypes frmManageApplicationtypes = new frmManageApplicationtypes();
            frmManageApplicationtypes.ShowDialog();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
        }

        private void frmMainScreen_Load(object sender, EventArgs e)
        {
        }        

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetaindLicense frmDetaindLicense = new frmDetaindLicense();
            frmDetaindLicense.ShowDialog();
        }

        private void manageDetainedLicencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagedetainLicenses frmManagedetainLicenses = new frmManagedetainLicenses();
            frmManagedetainLicenses.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicense frmReleasedDetainLicense = new frmReleasedDetainLicense();
            frmReleasedDetainLicense.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInetrnationalLicenses frmManageInetrnationalLicenses = new frmManageInetrnationalLicenses();
            frmManageInetrnationalLicenses.ShowDialog();
        }
    }
}
