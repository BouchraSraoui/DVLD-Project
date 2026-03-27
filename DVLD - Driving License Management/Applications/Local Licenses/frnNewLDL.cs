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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frnNewLDL : Form
    {
        

        private bool _isUpdated;
        private clsenAddUpdateMode _mode;
        private DataTable _licenseClassesTable;
        private clsPerson _person;

        private clsApplicationType _ApplicationType;
        private clsLicenseType _licenseType;
        private clsLocalDrivingLicenseApplication _localLicenseApp;


        public frnNewLDL(int LDLAppID = -1)
        {
            InitializeComponent();

            ctrlFilterBy1.Filter += _OnFilterPerson;

            _SetMode(LDLAppID);
        }

        #region Methods  

        private void _SetMode(int LDLAppID)
        {
            if (LDLAppID == -1)
            {
                _mode = clsenAddUpdateMode.ADD;
            }
            else
            {
                _mode = clsenAddUpdateMode.UPDATE;
                _localLicenseApp = clsLocalDrivingLicenseApplication.FindLDApp(LDLAppID);
                _person = _localLicenseApp.Application.Person;
            }
        }
        private clsPerson ReturnPerson(string Filter, object Field)
        {
            int PersonID = -1;
            if (Filter == "NationalNo")
            {
                PersonID = clsPerson.IsPersonExist(Field.ToString());
            }
            else
            {
                PersonID = Convert.ToInt32(Field.ToString());

                if (PersonID != -1)
                {
                    clsPerson Person = clsPerson.FindPersonByID(PersonID);

                    if (Person != null)
                    {
                        return Person;
                    }
                }
            }
            return null;
        }
        private void _OnFilterPerson(string Filter, object Field)
        {

            clsPerson person = ReturnPerson(Filter, Field);

            if (person == null)
            {
                MessageBox.Show($"Person with {Filter} = {Field} not found",
                                "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _person = person;
            ctrlShowPersonInfo1.FillControls(person);
            btnNext.Visible = true;
        }

        private void _InitializeAddMode()
        {
            tabControl1.TabPages.Remove(tabPage2);
            _InitializeFilterControl();
            btnNext.Visible = false;
            btnSave.Visible = false;
        }

        private void _InitializeUpdateMode()
        {
            tabControl1.TabPages.Remove(tabPage1);
            _FillLicenseClasses();

            _ApplicationType = clsApplicationType.GetApplicationInfoByIDType((int)enApplicationType.New_Local_Driving_License_Service);

            _DisplayUpdateInfo();
            btnSave.Visible = true;
        }

        private void _DisplayUpdateInfo()
        {
            lblShowTitle.Text = "Update Local Driving License";
            lblShowID.Text = _localLicenseApp.LocalDrivingLicenseApplicationID.ToString();

            var app = _localLicenseApp.Application;
            lblShowDate.Text = app.ApplicationDate.ToString();
            lblShowFees.Text = app.PaidFees.ToString();
            lblShowUser.Text = app.CreatedByUserID.ToString();
            comboBox1.Text = _localLicenseApp.LicenseClass.ClassName;
        }

        private void _FillLicenseClasses()
        {
            _licenseClassesTable = clsLicenseType.GetAllLicenseClasses();

            comboBox1.Items.Clear();
            foreach (DataRow row in _licenseClassesTable.Rows)
            {
                comboBox1.Items.Add(row["ClassName"]);
            }
        }

        private void _InitializeFilterControl()
        {
            ctrlFilterBy1.FilterBy = new Dictionary<string, string> { 
                                                  { "Person ID","int" },
                                                  { "National No", "string" } };
            ctrlFilterBy1.InitializeControl();
        }

        private void _ProceedToLicenseSelection()
        {
            ctrlFilterBy1.Enabled = false;
            tabControl1.TabPages.Add(tabPage2);
            tabControl1.SelectedTab = tabPage2;

            _FillLicenseClasses();

            lblShowDate.Text = DateTime.Now.ToString("d");
            lblShowUser.Text = clsSessionInfo.User.UserName;

            _ApplicationType = clsApplicationType.GetApplicationInfoByIDType((int)enApplicationType.New_Local_Driving_License_Service);
            lblShowFees.Text = _ApplicationType.ApplicationTypeFees.ToString();

            btnSave.Visible = true;
            btnNext.Visible = false;
        }
      
        private void _SaveApplication()
        {

            int Age = _person.Age;
            if (Age < _licenseType.MinimumAllowedAge)
            {
                MessageBox.Show($@"This person connot have License of type {_licenseType.ClassName}
                                 becouse Person Age = {Age} is less than License Class Age = {_licenseType.MinimumAllowedAge}",
                                 "Person Age Error",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return;
            }

            if (clsApplication.DoesPersonHaveSameActiveLicenseType(_person.PersonID,
                                                                   _ApplicationType.ApplicationTypeID,
                                                                   _licenseType.LicenseClassId))
            {
                MessageBox.Show("This person already has an active Application of the same type.");
                return;
            }

            if(clsLicense.IsLicenseExistsByPersonID(_person.PersonID, _licenseType.LicenseClassId))
            {
                MessageBox.Show("This person already has an active license of the same type.");
                return;
            }


            if (_mode == clsenAddUpdateMode.UPDATE && !_isUpdated)
                return;

            clsLocalDrivingLicenseApplication ldApplication = _BuildLicenseApplication();

            if (ldApplication == null)
            {
                MessageBox.Show("Failed to create Application.");
                return;
            }

            if (ldApplication.Save())
            {
                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _HandlePostSave(ldApplication);
            }

            _isUpdated = false;
        }


        private void _HandlePostSave(clsLocalDrivingLicenseApplication app)
        {
            if (_mode == clsenAddUpdateMode.ADD)
            {
                _mode = clsenAddUpdateMode.UPDATE;
                _localLicenseApp = app;
                _DisplayUpdateInfo();
            }
        }

        private clsLocalDrivingLicenseApplication _BuildLicenseApplication()
        {

            if (_mode == clsenAddUpdateMode.ADD)
            {
                clsApplication app = new clsApplication(_ApplicationType,_person, clsSessionInfo.User);
                if (!app.Save())
                    return null;

                return new clsLocalDrivingLicenseApplication(app, _licenseType);
            }
            else
            {
                _localLicenseApp.LicenseClass = _licenseType;
                return _localLicenseApp;
            }
        }

        #endregion

        #region Events
        private void frnNewLDL_Load(object sender, EventArgs e)
        {
            if (_mode == clsenAddUpdateMode.ADD)
                _InitializeAddMode();
            else
                _InitializeUpdateMode();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a license class.");
                return;
            }

            _SaveApplication();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _ProceedToLicenseSelection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _licenseType = clsLicenseType.GetLicenseClass(comboBox1.SelectedIndex + 1);

            if (_mode == clsenAddUpdateMode.UPDATE)
                _isUpdated = true;
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
       
            frmADDUpdatePerson frmADDUpdatePerson = new frmADDUpdatePerson();
            frmADDUpdatePerson.PersonSaved += ctrlShowPersonInfo1.FillControls;

            frmADDUpdatePerson.ShowDialog();

            ctrlFilterBy1.ComboboxSelectedItem = "Person ID";
            ctrlFilterBy1.TextBoxValue = ctrlShowPersonInfo1.PersonID.ToString();

        }

        #endregion

    }
}
