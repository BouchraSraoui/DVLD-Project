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

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmManageApplicationtypes : Form
    {
        public delegate bool IsUpdated();
        public static IsUpdated GetUpdatedStatus;
        public frmManageApplicationtypes()
        {
            InitializeComponent();
        }

        private void _ListAllApplicationTypes()
        {
            dataGridView1.DataSource = clsApplicationType.GetAllApplicationTypes();
        }
        private void frmManageApplicationtypes_Load(object sender, EventArgs e)
        {
            _ListAllApplicationTypes();

            lblShowApplicationtypesNumber.Text = dataGridView1.Rows.Count.ToString();
        }

        private void showApplicationTypeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmApplicationTypesEdit frmApplicationTypesEdit = new frmApplicationTypesEdit(ApplicationTypeID);

            frmApplicationTypesEdit.ApplicationTypeChanged += _ListAllApplicationTypes;

            frmApplicationTypesEdit.ShowDialog();

        }
    }
}
