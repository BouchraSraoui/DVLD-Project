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
    public partial class frmUpdateTestType : Form
    {
        private clsTestType _TestType = null;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestType=clsTestType.GetTestType(TestTypeID);
        }

        private void _DisplayTestTypeInfo()
        {
            lblShowId.Text = _TestType.TestTypeID.ToString();
            txtEnterTitle.Text = _TestType.TestTypeTitle;
            txtEnterFees.Text = _TestType.TestTypeFees.ToString();
            txtEnterDescription.Text = _TestType.TestTypeDescription;
        }
        private bool _SetErrors()
        {
            List<Control> controls = new List<Control>() { txtEnterDescription,
                                                           txtEnterFees,
                                                           txtEnterTitle};

            int counter = 0;
            foreach (Control control in controls)
            {
                if (string.IsNullOrEmpty(control.Text))
                {
                    errorProvider1.SetError(control, "Empty");
                    counter++;
                }
                else
                {

                    errorProvider1.SetError(control, "");
                }
            }
            return counter != 0;
        }
        private bool _IsChanged()
        {
            return (_TestType.TestTypeFees.ToString() != txtEnterFees.Text
                   ||
                   _TestType.TestTypeTitle != txtEnterTitle.Text
                   ||
                   _TestType.TestTypeDescription != txtEnterDescription.Text);
        }
        private void _UpdateTestTypeInfo()
        {
            if (_SetErrors())
                return;

            if (!_IsChanged())
                return;

            _TestType.TestTypeTitle = txtEnterTitle.Text;
            _TestType.TestTypeFees = Convert.ToInt64(txtEnterFees.Text);
            _TestType.TestTypeDescription = txtEnterDescription.Text;

            if (_TestType.Save())
                MessageBox.Show("Saved Data successfully");
            else
                MessageBox.Show("Faild");

        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _DisplayTestTypeInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _UpdateTestTypeInfo();
        }
    }
}
