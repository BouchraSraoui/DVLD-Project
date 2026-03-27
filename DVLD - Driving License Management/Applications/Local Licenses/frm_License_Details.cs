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

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frm_License_Details : Form
    {
        private int _LicenseID = -1;
        LicenseType _LicenseType;
        public frm_License_Details(int LicenseID , LicenseType LicenseType )
        {
            InitializeComponent();

            _LicenseID = LicenseID;
            _LicenseType = LicenseType;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frm_License_Details_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseDetails1.DisplayDetails(_LicenseID , _LicenseType);
        }

        private void ctrlDrivingLicenseDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
