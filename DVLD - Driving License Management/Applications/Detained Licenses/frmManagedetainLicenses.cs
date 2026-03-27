using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using DVLD___Driving_License_Management.Controls;
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

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmManagedetainLicenses : Form
    {
        public frmManagedetainLicenses()
        {
            InitializeComponent();
            ctrlFilterBy1.Filter += _Filter;
        }


        private void _ListAllDetainedLicenses()
        {
            dataGridView1.DataSource = clsDetainedLicense.GetAllDetainedLicenses();

            lblShowRecords.Text = dataGridView1.Rows.Count.ToString();
        }
        private void _Filter(string Filter, object Field)
        {
            if (Filter == "None")
            {
                _ListAllDetainedLicenses();
                return;
            }

            dataGridView1.DataSource = clsDetainedLicense.Filter(Filter, Field);

        }
        private int _GetSelectedLicenseID()
        {
            int LicenseID_COLUMN = 1;
            return (int)dataGridView1.CurrentRow.Cells[LicenseID_COLUMN].Value;
        }

        #region Events
        private void frmManagedetainLicenses_Load(object sender, EventArgs e)
        {
            _ListAllDetainedLicenses();

            ctrlFilterBy1.FilterBy = new Dictionary<string, string>
                                                     {{"None","" },
                                                      {"LicenseID","int" },
                                                      {"DetainID","int" },
                                                      {"NationalNo","string" },
                                                      {"ReleaseApplicationID","int" },
                                                      {"IsReleased","bool" }
                                                                  };

            ctrlFilterBy1.InitializeControl();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsLicense.GetLicense(_GetSelectedLicenseID()).Application.Person;

            frmPersonDetails frmPersonDetails = new frmPersonDetails(Person);
            frmPersonDetails.ShowDialog();

            _ListAllDetainedLicenses();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_License_Details frm_License_Details = new frm_License_Details(_GetSelectedLicenseID(),
                                                                              LicenseType.LocalLicense);

            frm_License_Details.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsDriver Driver = clsLicense.GetLicense(_GetSelectedLicenseID()).Driver;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(Driver);
            frmLicenseHistory.ShowDialog();

            _ListAllDetainedLicenses();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = (bool)dataGridView1.CurrentRow.Cells[4].Value;

            contextMenuStrip1.Items[4].Enabled = !IsReleased;
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicense frmReleasedDetainLicense = new frmReleasedDetainLicense(_GetSelectedLicenseID());
            frmReleasedDetainLicense.ShowDialog();

            _ListAllDetainedLicenses();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDetaindLicense frmDetaindLicense = new frmDetaindLicense();
            frmDetaindLicense.ShowDialog();

            _ListAllDetainedLicenses();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void ctrlFilterBy1_Load(object sender, EventArgs e)
        {

        }
    }
}
