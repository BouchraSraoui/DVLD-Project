using DVLD___Driving_License_Management.Controls;
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
using BusinessLayerDVLD;
namespace DVLD___Driving_License_Management.Users
{
    public partial class frmAddUpdateUser : Form
    {
        private clsPerson _Person = null;
        private clsUser _User = null;
        private string _PreviousUserName = "";
        enum enMode
        {
            Add,
            Update
        }

        enMode _Mode;
        public frmAddUpdateUser(int UserID =-1)
        {
            InitializeComponent();

            ctrlFilterBy1.Filter += _ShowPersonInfoAfterFilter;

            if (UserID == -1)
                _Mode = enMode.Add;
            else
            {
                _Mode = enMode.Update;
                _User = clsUser.GetUserByID(UserID);
                _Person = _User.Person;
            }
               
        }

        #region Methods

        private bool _UserNameIsUnique()
        {

            string UserName = txtUserName.Text;

            if (_Mode == enMode.Update && _PreviousUserName == UserName)
                return true;


            int UserID = clsUser.FindUserByUserName(UserName);

            if (UserID != -1)
                return false;


            return true;
        }
        public clsPerson _Filter(string Filter, object Field)
        {
            int PersonID;


            if (Filter == "NationalNo")
            {
                PersonID = clsPerson.IsPersonExist(Field.ToString());
            }
            else
            {
                PersonID = Convert.ToInt32(Field.ToString());
            }

            if (PersonID != -1)
            {
                clsPerson Person = clsPerson.FindPersonByID(PersonID);

                if (Person != null)
                {
                    return Person;
                }
            }
            return null;
        }
        private void _ShowPersonInfoAfterFilter(string Filter, object Field)
        {
            _Person = _Filter(Filter,Field);

                if (_Person != null)
                {
                ctrlShowPersonInfo1.FillControls(_Person);

                btnNext.Visible = true;

                    return;
                }

            btnNext.Visible = false;

            ctrlShowPersonInfo1._ResetControls();

            MessageBox.Show("The Person With " +
                                       Filter +
                                       " = " +
                                       Field +
                                       " Not Found",
                            "Person Not Found");
        }

        private void _ShowPersonInfoAfterAdd(clsPerson Person)
        {
            _Person = Person;
            if (_Person == null)
                return;

            btnNext.Visible = true;

            _InitializeCtrlFilter();

            ctrlShowPersonInfo1.FillControls(_Person);
        }

        private bool _AreUserFieldsEmpty()
        {
            return string.IsNullOrEmpty(txtUserName.Text)
                ||
                string.IsNullOrEmpty(txtPassword.Text)
                ||
                string.IsNullOrEmpty(txtConfirmPassword.Text)
                ;
        }

        private clsUser _CreateUser()
        {
            clsUser User = (_Mode == enMode.Add) ?
                new clsUser(_Person) :
                _User;
            User.UserName = txtUserName.Text;
            User.Password = txtPassword.Text;
            User.ISActive = (chbIsActive.Checked) ? true : false;


           return User;
        }

        private void _Initialize_Form()
        {
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update User Info ";
                linkLabel1.Visible = false;
                _DisplayUserInfo();
            }
            else
                lblTitle.Text = "Add New User ";

            _InitializePersonInfoTab();
        }

        private void _InitializeCtrlFilter()
        {
            ctrlFilterBy1.TextBoxValue = _Person.PersonID.ToString();
            ctrlFilterBy1.ComboboxSelectedItem = "PersonID";

            if(_Mode == enMode.Update) 
                ctrlFilterBy1.Enabled = false;
        }

        private void _DisplayUserInfo()
        {
           
            ctrlShowPersonInfo1.FillControls(_Person);
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chbIsActive.Checked = _User.ISActive ? true : false;

            _PreviousUserName = _User.UserName;

            _InitializeCtrlFilter();
        }

        private bool _IsPasswordConfirmed()
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                label3.Text = "Verify your PassWord";
                return false;
            }
            return true;
        }

        private void _SaveUser()
        {
            if (btnSave.Visible = !_AreUserFieldsEmpty())
            {

                if (!_UserNameIsUnique())
                {
                    MessageBox.Show($"User With UserName = { txtUserName.Text} is Exist \n Enter an other UserName",
                                    "User Exist");
                    return;
                }

                if (_IsPasswordConfirmed())
                    label3.Text = "";
                else
                    return;
                

                clsUser User = _CreateUser();

                if (User.Save())
                {
                    MessageBox.Show("User With ID = " + User.UserID + " Saved Succefully",
                        "Add User");

                    _HandlePostSave(User);
                    return;
                }

            }

        }
        private void _HandlePostSave(clsUser User)
        {
            lblUserID.Text = User.UserID.ToString();

            if (_Mode == enMode.Add)
            {
                _Mode = enMode.Update;
                lblTitle.Text = "Update User Info";
                ctrlFilterBy1.Enabled = false;
            }

            
        }

        private void _InitializePersonInfoTab()
        {
            if(_Mode == enMode.Add)
                btnNext.Visible = false;

            tabControl1.TabPages.Remove(tabPage2);
            //tabPage2.Hide();
            btnSave.Visible = false;
        }

        private void _InitializeLogintab()
        {
            tabControl1.TabPages.Add(tabPage2);
            //tabPage2.Show();
            tabControl1.SelectedTab = tabPage2;

            btnSave.Visible = true;
        }

        #endregion

        #region Events
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            ctrlFilterBy1.FilterBy = new Dictionary<string, string>
                                                     {
                                                       {"PersonID","int" },
                                                       { "NationalNo","stirng" }
                                                     };

            ctrlFilterBy1.InitializeControl();

            _Initialize_Form();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Person == null)
            {
                MessageBox.Show("Select a person first.");
                return;
            }


            if ( _Mode == enMode.Add && clsUser.isUser(_Person.PersonID) )
            {
                MessageBox.Show("Selected Person already has a user , choose another one",
                                 "Select another Person",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return;
            }
            _InitializeLogintab();
           
            btnNext.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _SaveUser();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
                return;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmADDUpdatePerson frmADDUpdatePerson = new frmADDUpdatePerson();

            frmADDUpdatePerson.PersonSaved += _ShowPersonInfoAfterAdd;
            frmADDUpdatePerson.ShowDialog();
        }
        #endregion
    }
}
