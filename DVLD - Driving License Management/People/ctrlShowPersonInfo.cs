using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;
namespace DVLD___Driving_License_Management.Controls
{
    public partial class ctrlShowPersonInfo : UserControl
    {
        private clsPerson _Person = null;
        public int PersonID
        {
            get
            {
                return _Person.PersonID;
            }
        }
        public ctrlShowPersonInfo()
        {
            InitializeComponent();
        }
        public void _ResetControls()
        {
            lblShowPersonId.Text = "???";
            lblShowNationalNo.Text = "???";
            lblShowAddress.Text = "???";
            lblShowEmail.Text = "???";
            lblShowName.Text = "???";
            lblShowPhone.Text = "???";
            lblShowGender.Text = "???";
            lblShowDateOfBirth.Text = "???";
            lblShowCountryName.Text = "???";
            pictureBox1.Image = Properties.Resources.person_man__1_;
        }

        public void FillControls(clsPerson Person)
        {
            lblShowPersonId.Text = Person.PersonID.ToString();
            lblShowNationalNo.Text = Person.NationalNo;
            lblShowAddress.Text = Person.Address;
            lblShowEmail.Text = Person.Email;
            lblShowName.Text = Person.FullName;
            lblShowPhone.Text = Person.PhoneNumber;
            lblShowGender.Text = Person.Gendor;
            lblShowDateOfBirth.Text=Person.DateOfBirth.ToString();
            lblShowCountryName.Text = clsCountry.GetCountryName(Person.CountryId);

            string ImagePath = Person.ImagePath.Replace(clsPath.filepath + "\\", ""); // Get file name

            if (!string.IsNullOrEmpty(ImagePath))
            {
                string fullPath = Path.Combine(clsPath.filepath, ImagePath);
                if (File.Exists(fullPath))
                {
                    pictureBox1.Load(fullPath);
                }
            }

            llblEditInfo.Visible = true;

            _Person = Person;
        }

        private void llblEditInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person == null)
                return;

            frmADDUpdatePerson frmADDUpdatePerson = new frmADDUpdatePerson(_Person);
            frmADDUpdatePerson.PersonSaved += FillControls;
            frmADDUpdatePerson.ShowDialog();

            
        }

        private void ctrlShowPersonInfo_Load(object sender, EventArgs e)
        {
            llblEditInfo.Visible = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
