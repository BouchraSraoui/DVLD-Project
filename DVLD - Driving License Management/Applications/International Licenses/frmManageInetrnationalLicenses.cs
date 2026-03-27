using BusinessLayerDVLD;
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

namespace DVLD___Driving_License_Management.Applications.InternationalLicenses
{
    public partial class frmManageInetrnationalLicenses : Form
    {
        public frmManageInetrnationalLicenses()
        {
            InitializeComponent();

            ctrlFilterBy1.Filter += _Filter;
        }

        #region Methods
        private void _ListAllIntLicneses()
        {
            dataGridView1.DataSource = clsInternationalLicense.GetAllIntLicenses();
        }


        private object _GetCellValue(int columnIndex)
        {
            return dataGridView1.CurrentRow.Cells[columnIndex].Value;
        }
      
        private clsInternationalLicense _GetSelectedInternationalLicense()
        {
            int IntLicense = (int)_GetCellValue(columnIndex:0);
            return clsInternationalLicense.GetInterLicense(IntLicense);
        }
        private clsDriver _GetDriver()
        {
            clsInternationalLicense InternationalLicense = _GetSelectedInternationalLicense();
            
            return InternationalLicense != null ? InternationalLicense.LocalLicense.Driver : null;
        }
        private void _CancelledLicense()
        {
            if (MessageBox.Show("Are You Sure?") != DialogResult.OK)
                return;

            
            clsInternationalLicense InternationalLicense = _GetSelectedInternationalLicense();
            if( InternationalLicense == null )
                return ;

            if( InternationalLicense.Disactive())
            {
                _ListAllIntLicneses();
                MessageBox.Show("Success");

                return;
            }

            MessageBox.Show("Faild");
        }

        private void _ShowAddUpdateForm(int IntLicenseID = -1)
        {
            frmNewInternationalDrivingLicense frmNewInternationalDrivingLicense = new frmNewInternationalDrivingLicense(IntLicenseID);
            frmNewInternationalDrivingLicense.ShowDialog();

            _ListAllIntLicneses();
        }


        private void _Filter(string Filter, object field)
        {
            if(Filter == "None")
            { 
                _ListAllIntLicneses();
                return;
            }

            dataGridView1.DataSource = clsInternationalLicense.Filter(Filter, field);
        }
        #endregion




        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _ShowAddUpdateForm();
        }

        private void frmManageInetrnationalLicenses_Load(object sender, EventArgs e)
        {
            _ListAllIntLicneses();

            ctrlFilterBy1.FilterBy = new Dictionary<string, string>
                                               {
                                                   {"None", ""},
                                                   {"International License ID", "int"},
                                                   {"Application ID", "int" },
                                                   {"Driver ID", "int" },
                                                   {"Issued Using Local License ID", "int" },
                                                   {"Is Active", "bool"},
                                                   {"Created By User ID" ,"int"}
                                               };

            ctrlFilterBy1.InitializeControl();

        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IntLicenseID = (int)_GetCellValue(0);

            frmLocalDrivingLicenseApplicationDetails frmLocalDrivingLicenseApplication = new frmLocalDrivingLicenseApplicationDetails(IntLicenseID);
            frmLocalDrivingLicenseApplication.ShowDialog();
        }

        private void canceledLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _CancelledLicense();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDriver Driver = _GetDriver();
            if(Driver != null)
            {
                frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(Driver);
                frmLicenseHistory.ShowDialog();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IntLicenseID = (int)_GetCellValue(columnIndex:0);
            _ShowAddUpdateForm(IntLicenseID);
        }
        #endregion
    }
}
