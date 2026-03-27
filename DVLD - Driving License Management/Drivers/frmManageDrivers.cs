using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Applications;
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

namespace DVLD___Driving_License_Management.Drivers
{
    public partial class frmManageDrivers : Form
    {
        clsDriver _Driver = null;
        public frmManageDrivers()
        {
            InitializeComponent();
            ctrlFilterBy1.Filter += _Filter;
        }

        #region methods
        private object _ReturnCurrentCellValue(int ColumnNumber)
        {
            object Value  = -1;
            if (dataGridView1.CurrentRow != null)
            {
                Value = Convert.ToInt32(dataGridView1.CurrentRow.Cells[ColumnNumber].Value);
            }
            return Value;
        }
        private void _ListAllDrivers()
        {
            dataGridView1.DataSource = clsDriver.GetAllDrivers();
        }
        private clsPerson _LoadPersonInfo()
        {
            int PersonID = (int)_ReturnCurrentCellValue(ColumnNumber :1);
            return clsPerson.FindPersonByID(PersonID);
        }
        private clsDriver _LoadDriverInfo()
        {
            int DriverID = (int)_ReturnCurrentCellValue(ColumnNumber: 0);
            return clsDriver.GetDriver(DriverID);
        }
        private void _Filter(string Filter , object Field)
        { 
            if (Filter == "None")
            {
                _ListAllDrivers();
                return;
            }
            dataGridView1.DataSource= clsDriver.Filter(Filter, Field);
        }

        private bool _DriverHasInternationalLicense()
        {
            _Driver = _LoadDriverInfo();
            if (_Driver == null)
                return false;

            if (_Driver.Has_Active_International_License())
            {
                MessageBox.Show("This Driver Has Already Active International License",
                    "Has International License",
                    MessageBoxButtons.OK);
                return true;
            }

            return false;
        }
        #endregion

        #region Events
        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _ListAllDrivers();

            ctrlFilterBy1.FilterBy = new Dictionary<string, string>
                                                     { {"None","" },
                                                       {"PersonID","int" },
                                                       {"DriverID","int" },
                                                       { "NationalNo","string" }
                                                     };

            ctrlFilterBy1.InitializeControl();

            lblShowRecords.Text = dataGridView1.Rows.Count.ToString();

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            frmPersonDetails frmPersonDetails = new frmPersonDetails(_LoadPersonInfo());
            frmPersonDetails.ShowDialog();
            //hna lazmli event bah y3rfni idha srat update wlala
            _ListAllDrivers();
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DriverHasInternationalLicense())
                return;

            frmNewInternationalDrivingLicense frmNewInternationalDrivingLicense = 
                                                        new frmNewInternationalDrivingLicense(_Driver.GetLicense_Class3_OfDriver());
            frmNewInternationalDrivingLicense.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(_LoadDriverInfo());
            frmLicenseHistory.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion
    }
}
