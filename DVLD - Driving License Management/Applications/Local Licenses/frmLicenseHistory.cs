using BusinessLayerDVLD;
using BusinessLayerDVLD.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmLicenseHistory : Form
    {
       
  
        LicenseType _LicenseType = LicenseType.LocalLicense;

        clsDriver _Driver = null;

        clsPerson _Person = null;
        public frmLicenseHistory(clsDriver Driver)
        {
            InitializeComponent();

            _Driver = Driver;
            _Person = _Driver.Person;
        }

        #region Private Methods
        private void _SetupFilterControle()
        {
            ctrlFilterBy1.FilterBy = new Dictionary<string, string>
                                                 {
                                                     { "PersonID","int" }
                                                 };

            ctrlFilterBy1.InitializeControl();


            ctrlFilterBy1.TextBoxValue= _Driver.PersonID.ToString();

            ctrlFilterBy1.Enabled = false;

        }

        private void _ShowPersonDetails()
        {
            _SetupFilterControle();

            ctrlShowPersonInfo1.FillControls(_Person);

            _GetAllLocalLicenses();
        }

        private object _GetCellValue(int columnIndex)
        {
            object value = dataGridView1.CurrentRow?.Cells[columnIndex].Value;

            if (_LicenseType == LicenseType.InternationalLicense)
                value = dataGridView2.CurrentRow?.Cells[columnIndex].Value;


            return value;

        }

        private void _GetAllLocalLicenses()
        {
            dataGridView1.DataSource = _Driver.GetAllLicensesOfDriver();

            _ShowRecords();
            _GetAllInternationalLicenses();
        }

        private void _ShowRecords()
        {
            int count;
            if (tabControl1.SelectedTab == tabPage1)
                count = dataGridView1.Rows.Count;
            else
            {
                count = dataGridView2.Rows.Count;

                _LicenseType = LicenseType.InternationalLicense;
            }

            lblShowRecords.Text = count.ToString();
        }

        private void _ConcatenationDataTables( DataTable dt)
        {
            DataTable existingData = (DataTable)dataGridView2.DataSource;

            foreach (DataRow newRow in dt.Rows)
            {
                existingData.ImportRow(newRow);
            }

            dataGridView2.DataSource = existingData;
        }

        private bool _Validity_Class_Acive_License(DataGridViewRow row)
        {
            string NormalLicenseID = "Class 3 - Ordinary driving license";
            bool IsActive = Convert.ToBoolean(row.Cells["IsActive"].Value);


            return (row.Cells["ClassName"].Value.ToString() == NormalLicenseID && IsActive);
        }

        private void _GetAllInternationalLicenses()
        { 
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                object LicenseId = row.Cells["LicenseID"].Value;
                int LocalLicenseID = -1;

                if(LicenseId !=null && int.TryParse(LicenseId.ToString(),out LocalLicenseID))
                {
                    if (!_Validity_Class_Acive_License(row))
                        continue;

                    DataTable dtInterLicenses = clsInternationalLicense.GetInterLicenseOFLocalLicense(LocalLicenseID); 

                    if (dtInterLicenses == null)
                        continue;

                    if (dataGridView2.Rows.Count == 0)
                    {
                        dataGridView2.DataSource = dtInterLicenses;
                    }
                    else
                    {
                        _ConcatenationDataTables(dtInterLicenses);
                    }
                }
            }

           
        }

        #endregion

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {

            _ShowPersonDetails();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ShowRecords();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LICENSEID_COLUMN = 0;
            int LicenseID = (int)_GetCellValue(LICENSEID_COLUMN);

            frm_License_Details frm_License_Details = new frm_License_Details(LicenseID: LicenseID,
                                                                              _LicenseType);
            frm_License_Details.ShowDialog();

        }
        #endregion

        private void ctrlShowPersonInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
