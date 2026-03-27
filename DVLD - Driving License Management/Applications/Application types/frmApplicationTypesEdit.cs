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
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DVLD___Driving_License_Management.Global_Classes;

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmApplicationTypesEdit : Form
    {
        clsApplicationType _ApplicationType = null;

        public event Action ApplicationTypeChanged;
        public frmApplicationTypesEdit(int ApplicationtypeID)
        {
            InitializeComponent();

           _ApplicationType = clsApplicationType.GetApplicationInfoByIDType(ApplicationtypeID);
        }

        private bool _IsChanged()
        {
            return (_ApplicationType.ApplicationTypeFees.ToString() != txtenterFees.Text
                   ||
                   _ApplicationType.ApplicationTypeTitle != txtEnterTitle.Text);
        }

        private void _DisplayAppTypeInfo()
        {
            if( _ApplicationType != null )
            {
                lblshowId.Text = _ApplicationType.ApplicationTypeID.ToString();
                txtEnterTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtenterFees.Text = _ApplicationType.ApplicationTypeFees.ToString();
            }
        }

        private void _UpdateApplicationTypeInfo()
        {
            _ApplicationType .ApplicationTypeTitle  = txtEnterTitle.Text;
            _ApplicationType.ApplicationTypeFees = Convert.ToInt64(txtenterFees.Text);

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Saved Data successfully");
                ApplicationTypeChanged?.Invoke();
            }
               
            else
                MessageBox.Show("Faild");

        }

        private void frmApplicationTypesEdit_Load(object sender, EventArgs e)
        {
            _DisplayAppTypeInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_IsChanged())
                return;

            _UpdateApplicationTypeInfo();
        }
  
        private void txtEnterTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnterTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEnterTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtEnterTitle, null);
            };

        }

        private void txtenterFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtenterFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtenterFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtenterFees, null);
            };


            if (!clsValidation.IsNumber(txtenterFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtenterFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtenterFees, null);
            };
        }
    }
}
