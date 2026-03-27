using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Users;
using System;
using System.Windows.Forms;

namespace DVLD___Driving_License_Management.Applications
{
    public partial class frmScheduleTest : Form
    {
        clsTestAppointments _Appointment = null;
        int _Trial = 0;
        public frmScheduleTest(clsTestAppointments Appointment , int Trial)
        {
            InitializeComponent();

            _Appointment = Appointment;
            _Trial = Trial;
            frmTestAppointment.GetTest += _CreateNewTest;
        }
        private clsTest _CreateNewTest()
        {
            clsTest test = new clsTest(_Appointment, clsSessionInfo.User);
            test.Notes = textBox1.Text;
            test.TestResult = rbtnPass.Checked ? true : false;

            return test;
        }

        private bool _SetErrors()
        {
            if(!rbtnFail.Checked && !rbtnPass.Checked)
            {
                errorProvider1.SetError(lblResults, "Select Result Please");
                return true;
            }
            else
            {
                errorProvider1.SetError(lblResults, "");
                return false;
            }
        }

        private void _AddTest()
        {
            if (_SetErrors())
                return;

            clsTest test = _CreateNewTest();

            if (test.Save())
            {
                 
                ctrlScheduleTest1.TestID = test.TestID.ToString();

                if(_Appointment.Locke())
                    MessageBox.Show("Data Saved Successfully");
               
                rbtnFail.Enabled = rbtnPass.Enabled = textBox1.Enabled = false;
            }
               
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeVisiontest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1._ShowAppointmentInfo(_Appointment, 
                                                   _Trial ,
                                                   IsTakeTest: true);
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _AddTest();

            btnSave.Enabled = false;
        }

        private void ctrlScheduleTest1_Load(object sender, EventArgs e)
        {

        }


    }
}
