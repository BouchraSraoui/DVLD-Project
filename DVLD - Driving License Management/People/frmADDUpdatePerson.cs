using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Controls;
using DVLD___Driving_License_Management.Global_Classes;
using DVLD___Driving_License_Management.Persons;
using DVLD___Driving_License_Management.Users;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace DVLD___Driving_License_Management
{
    public partial class frmADDUpdatePerson : Form
    {
        public event Action<clsPerson> PersonSaved;
        public event Action IsNewPersonSaved;  
        enum enMode { Add, Update };
        enMode _Mode;

        private string _LastNationalNo;
        private string _ImagePath = "";
        private clsPerson _Person;
        public frmADDUpdatePerson(clsPerson Person =null)
        {
            InitializeComponent();

            if (Person == null)
                _Mode = enMode.Add;
            else
            {
                _Mode = enMode.Update;
                _Person = Person;
            }

           
        }

        #region Methods
        private bool _SetError(Control control, bool Condition , string msg)
        {
            if (!Condition)
            {
                errorProvider1.SetError(control, msg);
                return true;
            }
            errorProvider1.SetError(control,"");
            return false;
        }

        private void _FillCountries()
        {
            DataTable Countries = clsCountry.GetAllCountries();

            foreach (DataRow row in Countries.Rows)
            {
                comboBox1.Items.Add(row["CountryName"].ToString());
            }
        }

        private void _DisplayPersonInfo()
        {

            if (_Person == null)
                return;

            lblShowPersonID.Text = _Person.PersonID.ToString();
            txtAddress.Text = _Person.Address;
            txtEmail.Text = _Person.Email;
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtPhone.Text = _Person.PhoneNumber;
            txtThirdName.Text = _Person.ThirdName;
            txtSecondName.Text = _Person.SecondName;

            if(_Person.ImagePath != clsPath.filepath)
            {
                _ImagePath = pictureBox1.ImageLocation = _Person.ImagePath;
            }
            comboBox1.SelectedIndex = _Person.CountryId - 1;
            if (_Person.Gendor == "Male")
                rbtnMale.Checked = true;
            else
                rbtnFmale.Checked = true;

            _LastNationalNo = _Person.NationalNo;


            llblRemove.Visible = _Person.ImagePath != clsPath.filepath;
        }

        private void _Initialize_Form()
        {
            _FillCountries();


            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Person Info ";
                _DisplayPersonInfo();
            }
            else
            {
                lblTitle.Text = "Add New Person ";
                llblRemove.Visible = false;
            }
        }

        private bool _ValidateControls(Control control, bool Condition, string msg)
        {
           
           if( _SetError(control,
                         Condition,
                         msg))
           {
                btnSave.Enabled = false;
                return true;
           }
                
            btnSave.Enabled = true;
            return false;
        }
       
        private bool _NationalNoIsUnique()
        {

            string NationalNo = txtNationalNo.Text;

            if (_Mode == enMode.Update && _LastNationalNo == NationalNo)
                return true;


            int PersonID = clsPerson.IsPersonExist(NationalNo);

            if (PersonID != -1)
                return false;
            

            return true;
        }

        private bool _SetErrorProviderForEmptyControls()
        {
            List<Control> controls = new List<Control>() { txtAddress,
                                                           txtFirstName,
                                                           txtLastName,
                                                           txtNationalNo,
                                                           txtPhone,
                                                           comboBox1,
                                                           dateTimePicker1};

            int counter = 0;
            foreach (Control control in controls)
            {
                if (_SetError(control, 
                    Condition:!string.IsNullOrEmpty(control.Text), 
                    msg:"Empty"))

                    counter++;

            }

            if (_SetError(groupBox1,
                   Condition: !(rbtnFmale.Checked == false && rbtnMale.Checked == false),
                   msg: "Empty"))

                counter++;

            return counter != 0;
        }

        private clsPerson _CreateNewPerson()
        {
            clsPerson person = (_Mode == enMode.Add) ?
                new clsPerson() :
                _Person;

            person.Address = txtAddress.Text;
            person.Gendor = rbtnFmale.Checked ? "Female" : "Male";
            person.DateOfBirth = dateTimePicker1.Value;
            person.PhoneNumber = txtPhone.Text;
            person.NationalNo = txtNationalNo.Text;
            person.Email = txtEmail.Text;
            person.FirstName = txtFirstName.Text;
            person.LastName = txtLastName.Text;
            person.SecondName = txtSecondName.Text;
            person.ThirdName = txtThirdName.Text;
            person.CountryId = comboBox1.SelectedIndex + 1;

            if(person.ImagePath != _ImagePath)
                person.ImagePath = Path.Combine(clsPath.filepath, _HandleImage(_ImagePath));

            return person;
        }

        private bool _PersonInfoIsUpdated()
        {

            return txtAddress.Text != _Person.Address
                || txtEmail.Text != _Person.Email
                || txtFirstName.Text != _Person.FirstName
                || txtLastName.Text != _Person.LastName
                || txtNationalNo.Text != _Person.NationalNo
                || txtPhone.Text != _Person.PhoneNumber
                || txtThirdName.Text != _Person.ThirdName
                || txtSecondName.Text != _Person.SecondName
                || _ImagePath != _Person.ImagePath;

        }

        private void _SavePerson()
        {

            if (_SetErrorProviderForEmptyControls())
                return;

            //int PersonId = clsPerson.IsPersonExist(txtNationalNo.Text.Trim());
            //if (PersonId != -1)
                //return;
 
            if (_Mode == enMode.Update && !_PersonInfoIsUpdated())
                return;

            _Person = _CreateNewPerson();

            if (_Person.Save())
            {
                lblShowPersonID.Text = _Person.PersonID.ToString();
                MessageBox.Show("Data Saved Successfully",
                                 "Save Person",
                                 MessageBoxButtons.OK);


                IsNewPersonSaved?.Invoke();

                //if we have a user control ShowPersonInfo
                PersonSaved?.Invoke(_Person);
            }


            llblRemove.Visible = _Person.ImagePath != "";

        }

        private string _HandleImage(string FileName)
        {
            if(_Mode == enMode.Update)
             _DeleteImageFromSpecifyPath();

            return clsUtils.CopyImageToFolder( FileName, clsPath.filepath);
        }

        private bool _LoadImage()
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return false;

            _ImagePath = openFileDialog1.FileName;
          
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pictureBox1.Load(selectedFilePath);
                llblRemove.Visible = true;
                // ...
            
            return true;
        }

        private void _DeleteImageFromSpecifyPath()
        {
            if (_Person.ImagePath != _ImagePath)
            {
                try
                {
                    if (File.Exists(pictureBox1.ImageLocation))
                    {
                        File.Delete(pictureBox1.ImageLocation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
  
        }
        #endregion


        #region Events
        private void frmADDUpdatePerson_Load(object sender, EventArgs e)
        {
            _Initialize_Form();
            errorProvider1.ContainerControl = this;

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SavePerson();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                llblRemove.Visible = _LoadImage();
        }

        private void llblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.person_man__1_;
            _ImagePath = "";

            llblRemove.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;

           e.Cancel= _SetError(txtEmail,
                      Condition: clsValidation.validateEmail(txtEmail.Text),
                      msg: "Invalid Email Format");
        }

        private void txtPhone_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhone.Text))
                return;

            e.Cancel=_ValidateControls(txtPhone,
                    Condition: clsValidation.validatePhone(txtPhone.Text),
                    msg: "Phone Number must be contains only numbrs!!");
        }

        private void txtNationalNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNationalNo.Text))
                return;

            e.Cancel= _ValidateControls(txtNationalNo,
                   Condition: _NationalNoIsUnique(),
                   msg: "This National Is Used For another Person enter Again!!");
        }

        #endregion

    }
}


