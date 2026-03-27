using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Global_Classes;
using DVLD___Driving_License_Management.Persons;
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

namespace DVLD___Driving_License_Management.Applications.Controls
{
    public partial class ctrlBasicApplicationInfo : UserControl
    {
        clsApplication _Application = null;
        public ctrlBasicApplicationInfo()
        {
            InitializeComponent();
        }

        clsPerson _Person = null;

        public void ResetApplicationInfo()
        {


            lblShowID.Text = "[????]";
            lblShowStatus.Text = "[????]";
            lblShowType.Text = "[????]";
            lblShowFees.Text = "[????]";
            lblShowApplicant.Text = "[????]";
            lblShowDate.Text = "[????]";
            lblShowStatusDate.Text = "[????]";
            lblShowUser.Text = "[????]";

        }

        private void _FillApplicationInfo()
        {
            lblShowID.Text = _Application.ApplicationID.ToString();
            lblShowDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblShowStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblShowFees.Text = _Application.PaidFees.ToString();
            lblShowStatus.Text = _Application.Status();
            lblShowType.Text = _Application.ApplicationType.ApplicationTypeTitle;
            lblShowUser.Text = _Application.CreatedByUser.UserName;

            clsPerson Person = _Application.Person;
            if (Person != null)
            {
                lblShowApplicant.Text = Person.FullName;
                _Person = Person;
            }
        }
        public void DisplayApplicationDetails(clsApplication Application)
        {

            if (Application == null)
            {
                ResetApplicationInfo();

                MessageBox.Show("No Application ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Application = Application;

            _FillApplicationInfo();

          
        }
        private void ctrlBasicApplicationInfo_Load(object sender, EventArgs e)
        {
            ResetApplicationInfo();
        }

        private void llblPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails(_Person);
            frmPersonDetails.ShowDialog();

            //Refresh
            DisplayApplicationDetails(_Application);
        }

    }
}
