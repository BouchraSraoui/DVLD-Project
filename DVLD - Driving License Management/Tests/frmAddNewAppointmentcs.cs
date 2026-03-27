using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Users;
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
    
    public partial class frmAddNewAppointmentcs : Form
    {
        public delegate DateTime AddNewAppointmentcsEventHandler();
        public static event AddNewAppointmentcsEventHandler AddNewAppointmentcs;

        clsTestAppointments _appointment = null;
        clsApplicationType _ApplicationType = null;
        int _Trial = -1;

        enum enMode
        {
            Add,
            Update
        }

        enMode _Mode= enMode.Add;
        public frmAddNewAppointmentcs(clsTestAppointments Appointment, int Trial)
        {
            InitializeComponent();

            if(Appointment.TestAppointmentID == -1)
                _Mode = enMode.Add;
            else
                _Mode = enMode.Update;  
            _appointment = Appointment;
            _Trial = Trial;
        }

        private clsApplication _CreateRetakeTestApplication()
        {
            clsApplication RetakeTestApplication = new clsApplication(_ApplicationType,
                                                                      _appointment.LDLApp.Application.Person,
                                                                      clsSessionInfo.User);

            return RetakeTestApplication.Save() ? RetakeTestApplication : null;
        }
        private void _Retaketest()
        {
            groupBox1.Enabled = true;

            _ApplicationType = clsApplicationType.GetApplicationInfoByIDType((int)enApplicationType.Retake_Test);

            lblShowAppFees.Text = _ApplicationType.ApplicationTypeFees.ToString();
            lblShowTotalFees.Text = (_appointment.PaidFees + _ApplicationType.ApplicationTypeFees).ToString();
        }

        private bool _createNewAppointment()
        {
            if(_Trial > 0)
            {
                clsApplication RetakeTestApplication = _CreateRetakeTestApplication();
                if (RetakeTestApplication == null)
                    return false;

                _appointment.RetakeTestApplication = RetakeTestApplication;
            }

            _appointment.AppointmentDate = (DateTime)AddNewAppointmentcs?.Invoke();
            
            return _appointment.Save();
        }
        private void _InitForm()
        {
            ctrlScheduleTest1._ShowAppointmentInfo(_appointment, _Trial);

            if (_Trial > 0)
                _Retaketest();

            if (_Mode == enMode.Update && _appointment.ISLocked)
                btnSave.Enabled = false;
        }
        private void _SaveAppointment()
        {
            
            if (_createNewAppointment())
            {
                MessageBox.Show("Data Saved Successfully");

                if (groupBox1.Enabled == true)
                    lblShowRTestAppID.Text = _appointment.TestAppointmentID.ToString();

                btnSave.Enabled = false;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmAddNewAppointmentcs_Load(object sender, EventArgs e)
        {
            _InitForm();
        }
      

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            _SaveAppointment();
        }

        private void ctrlScheduleTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
