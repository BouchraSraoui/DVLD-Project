using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;

namespace DVLD___Driving_License_Management.Persons
{
    public partial class frmPersonDetails : Form
    {
        
        private clsPerson _Person = null;
        public frmPersonDetails(clsPerson person)
        {
            InitializeComponent();
            _Person = person;
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
            ctrlShowPersonInfo1.FillControls(_Person);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlShowPersonInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
