using BusinessLayerDVLD;
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

namespace DVLD___Driving_License_Management.Controls
{
    public partial class ctrlFilterByLicenseID : UserControl
    {
        public delegate void ctrlFilterByLicenseIDEventHandler(int LicenseID);
        public event ctrlFilterByLicenseIDEventHandler FilterByLicenseID;

        public int ShowLicenseID
        {
            set
            {
                txtPrintLicenseID.Text = value.ToString();

                _DisplayfilterResult();
            }
        }

        private clsLicense _License = null;
        public ctrlFilterByLicenseID()
        {
            InitializeComponent();
        }

        #region private methods
        private bool _ValidateLicenseInput()
        {
            if (string.IsNullOrEmpty(txtPrintLicenseID.Text))
            {
                MessageBox.Show("Please enter the license ID.", "Missing Value");
                return false;
            }

            int licenseID = -1;
            if (!int.TryParse(txtPrintLicenseID.Text, out licenseID))
            {
                MessageBox.Show("License ID must be a number.", "Invalid Input");
                return false;
            }
            else
            {
                if (licenseID < 0)
                {
                    MessageBox.Show("License ID must be a positive number.", "Invalid Input");
                    return false;
                }
            }

            return true;
        }

        private bool _LicenseIsActive()
        {
            if (!_License.ISActive)
            {
                FilterByLicenseID?.Invoke(_License.LicenseID);
                MessageBox.Show($"License with ID = {_License.LicenseID} is not active.", "Inactive License");
            }

            return _License.ISActive;
        }

        private bool _LoadLicense()
        {
            int licenseID = Convert.ToInt32(txtPrintLicenseID.Text);
            _License = clsLicense.GetLicense(licenseID);

            if( _License == null)
            {
                MessageBox.Show($"Faild", "Not Found");
                return false;
            }
            return true;
        }

        private void _DisplayfilterResult()
        {
            if (!(_ValidateLicenseInput() && _LoadLicense()))
                return;

            if (!_LicenseIsActive() )
                return;
            
            FilterByLicenseID?.Invoke(_License.LicenseID);
        }

        #endregion 
        private void ctrlFilterByLicenseID_Load(object sender, EventArgs e)
        {

        }

        private void txtPrintLicenseID_KeyDown_1(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _DisplayfilterResult();
            }
        }

        private void lblLicenseID_Click(object sender, EventArgs e)
        {

        }
    }
}
