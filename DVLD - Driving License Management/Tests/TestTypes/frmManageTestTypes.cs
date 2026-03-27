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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _ListAllTestTypes()
        {
            dataGridView1.DataSource = clsTestType.GetAllTestTypes();
        }

        
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _ListAllTestTypes();

            lblShowRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object TestTypeId = dataGridView1.CurrentRow.Cells[0].Value;
            if (TestTypeId == null)
                return;
           
            frmUpdateTestType frmUpdateTestType = new frmUpdateTestType((int)TestTypeId);
            frmUpdateTestType.ShowDialog();

            _ListAllTestTypes();

        }
    }
}
