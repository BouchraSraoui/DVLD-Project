using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {
        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();

            ctrlFilterBy2.Filter += _Filter;
        }


        #region Private Methods
        private clsApplication _GetSelectedApplication()
        {
            int LDLAppID = (int)_GetCellValue(0);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLDApp(LDLAppID);

            return localDrivingLicenseApplication?.Application ?? null;

        }

        private bool _DeleteLDLApp()
        {
            clsApplication Application = _GetSelectedApplication();

            if(Application == null)
                return false;


            if (Application.DeleteApplication())
                return true;

            return false;

        }

        private void _ShowTestScreen(int NumberOfTest)
        {
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLAppWithTestResult((int)_GetCellValue(0));
            clsTestType TestType = clsTestType.GetTestType(NumberOfTest);

            frmTestAppointment frmvisiontest = new frmTestAppointment(LDLApp, TestType);
            frmvisiontest.ShowDialog();

            _ListAllApplications();
        }

        private bool CancelApplication()
        {
            clsApplication Application = _GetSelectedApplication();
            if (Application == null)
                return false;

            bool saved = Application.Cancel();
            MessageBox.Show(saved ? 
                                  "Success" 
                                  : 
                                  "Faild");

            return saved;
        }

        private bool CanceledLDLApp()
        {
            int Status_Column = 6;
            //var status = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), GetCellValue(6).ToString());

            enApplicationStatus status;
            Enum.TryParse(_GetCellValue(Status_Column).ToString(), out status);

            switch(status)
            {
                case enApplicationStatus.New:
                    return CancelApplication();

                case enApplicationStatus.Cancelled:
                    MessageBox.Show("This License is already Canceled");
                    break;

                case enApplicationStatus.Completed:
                    MessageBox.Show("This Application is Completed You can not Canceled");
                    break;

            }
            return false;
        }

        private void _ListAllApplications()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.GetAllLDLApplication();
        }

        private object _GetCellValue(int columnIndex)
        {

            return dataGridView1.CurrentRow?.Cells[columnIndex].Value;
        }
       
        private void _ShowAddUpdatePage(int AppID)
        {
            frnNewLDL frmADDUpdateApplication = new frnNewLDL(AppID);
            frmADDUpdateApplication.ShowDialog();

            _ListAllApplications();
        }

        private int _GetPassedTests(int PASSED_TESTS_COLUMN)
        {

            int passesTests = Convert.ToInt32(dataGridView1.CurrentRow.Cells[PASSED_TESTS_COLUMN].Value);
            return passesTests;
        }

        private string _GetStatus(int STATUS_COLUMN)
        {
            string Status = (string)dataGridView1.CurrentRow.Cells[STATUS_COLUMN].Value;
            return Status;
        }

        private void _SetupSchudeleTests(int PassesTests, string Status)
        {
            ToolStripMenuItem mainItem = (ToolStripMenuItem)contextMenuStrip1.Items[6];

            int MENU_VISION = 0;
            int MENU_WRITTEN = 1;
            int MENU_STREET = 2;
            bool IsEnabled = PassesTests != 3 && Status == "New";

            if (mainItem.Enabled = IsEnabled)
            {
                mainItem.DropDownItems[MENU_VISION].Enabled = false;
                mainItem.DropDownItems[MENU_WRITTEN].Enabled = false;
                mainItem.DropDownItems[MENU_STREET].Enabled = false;

                if (PassesTests == 0)
                {
                    mainItem.DropDownItems[MENU_VISION].Enabled = true;
                }
                else if (PassesTests == 1)
                {
                    mainItem.DropDownItems[MENU_WRITTEN].Enabled = true;
                }
                else
                {
                    mainItem.DropDownItems[MENU_STREET].Enabled = true;
                }

            }
           
        }

        private void _SetupOtherMenuItems(int passedTests, string status)
        {
            const int MENU_ISSUE_LICENSE = 7;
            const int MENU_SHOW_LICENSE = 8;
            const int MENU_EDIT = 2;
            const int MENU_DELETE = 3;
            const int MENU_CANCEL = 4;

            contextMenuStrip1.Items[MENU_ISSUE_LICENSE].Enabled = (status == "New" && passedTests == 3);
            contextMenuStrip1.Items[MENU_SHOW_LICENSE].Enabled = status == "Completed";
            contextMenuStrip1.Items[MENU_EDIT].Enabled = status == "New";
            contextMenuStrip1.Items[MENU_DELETE].Enabled = status != "Completed";
            contextMenuStrip1.Items[MENU_CANCEL].Enabled = status != "Completed" && status != "Canceled";
        }

        private clsApplication _GetApplicationInfoByID()
        {
            int LDLAppID = (int)_GetCellValue(0);

            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.FindLDApp(LDLAppID);
            return LDLApp?.Application ?? null;
        }

        private clsDriver _GetDriverRelatedPerson()
        {
            int LDLID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLAppWithTestResult(LDLID);

            if (LDLApp == null)
            {
                MessageBox.Show("Application Not Found");
                return null;
            }

            int DriverID = clsDriver.isDriver(LDLApp.Application.PersonID);
            if (DriverID == -1)
            {
                MessageBox.Show("This Person is not a Driver yet");
                return null;
            }
               
            return clsDriver.GetDriver(DriverID);

        }

        private int GetLicenseID()
        {
            clsApplication Application = _GetApplicationInfoByID();
            if (Application == null)
                return -1;

            return clsLicense.GetLicenseIDByAPPId(Application.ApplicationID);
        }

        private void _Filter(string Filter, object field)
        {
            if(Filter == "None")
            {
                _ListAllApplications();
                return;
            }
            if(Filter == "Status")
            {
                field = (int)clsApplication.applicationStatus(field.ToString());
            }

            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.Filter(Filter, field);
        }

        #endregion

        #region Event 

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageApplications_Load(object sender, EventArgs e)
        {

            ctrlFilterBy2.FilterBy = new Dictionary<string, string>
                                                     { {"None","" },
                                                       { "L_DL_APPID","int" },
                                                       { "National No","string" },
                                                       { "Status","string" } };

            ctrlFilterBy2.InitializeControl();

            _ListAllApplications();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)_GetCellValue(0);

            if (AppID > -1)
                _ShowAddUpdatePage(AppID);
        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)_GetCellValue(0);

            frmLocalDrivingLicenseApplicationDetails frmLocalDrivingLicenseApplication = new frmLocalDrivingLicenseApplicationDetails(LDLAppID);
            frmLocalDrivingLicenseApplication.ShowDialog();

            _ListAllApplications();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure??") != DialogResult.OK)
                return;

            if (_DeleteLDLApp())
            {
                _ListAllApplications();
                MessageBox.Show("Success");

            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanceledLDLApp())
                _ListAllApplications();
            
        }

        private void sexualVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowTestScreen(NumberOfTest: 1);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int STATUS_COLUMN = 6;


            int passesTests = _GetPassedTests(PASSED_TESTS_COLUMN: STATUS_COLUMN - 1);
            string Status = _GetStatus(STATUS_COLUMN);
           

            _SetupSchudeleTests(passesTests, Status);
            _SetupOtherMenuItems(passesTests, Status);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowTestScreen(NumberOfTest: 2);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scheduleTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowTestScreen(NumberOfTest: 3);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)_GetCellValue(0);

            frm_Issue_Driving_License_First_Time frm_Issue_Driving_License_First_Time = new frm_Issue_Driving_License_First_Time(AppID);
            frm_Issue_Driving_License_First_Time.ShowDialog();
            
            _ListAllApplications();
            
                
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = GetLicenseID();  
            if (LicenseID == -1)
            {
                MessageBox.Show("License Doesn't existe");
                return;
            }

            frm_License_Details frm_License_Details = new frm_License_Details(LicenseID,
                                                                              LicenseType.LocalLicense);
            frm_License_Details.ShowDialog();

        }

        private void showPersonLicensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDriver Driver = _GetDriverRelatedPerson();

            if (Driver == null)
                return;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(Driver);
            frmLicenseHistory.ShowDialog();

        }

        private void btnAddApplication_Click(object sender, EventArgs e)
        {
            frnNewLDL frnNewLDL = new frnNewLDL();
            frnNewLDL.ShowDialog();

            _ListAllApplications();
        }
        #endregion


    }
}
