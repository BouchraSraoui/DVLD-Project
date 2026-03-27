using BusinessLayerDVLD;
using DVLD___Driving_License_Management.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD___Driving_License_Management.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
       
        public string TestID
        {
            set
            {
                lblShowTestID.Text = value;
            }
        }   
        public ctrlScheduleTest()
        {
            InitializeComponent();
            frmAddNewAppointmentcs.AddNewAppointmentcs += _returnAppoimentDate;
        }

        private DateTime _returnAppoimentDate()
        {
            return dateTimePicker1.Value;
        }

        private void _ChangeTitle(int Trial)
        {
            if (Trial > 0)
                lblShowtitle.Text = "Schedule Retake test";
        }
        public void _ShowAppointmentInfo(clsTestAppointments appointment,
                                         int Trial=0,
                                         bool IsTakeTest = false)
        {

            _ChangeTitle(Trial);

            clsLocalDrivingLicenseApplication LDLApp = appointment.LDLApp;
            lblShowDClass.Text = LDLApp.LicenseClass.ClassName;
            lblShowLDLAppID.Text = LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblShowName.Text = LDLApp.Application.Person.FullName;
            lblShowTrial.Text = Trial.ToString();
            lblShowFees.Text = appointment.TestType.TestTypeFees.ToString();

            dateTimePicker1.Enabled = !(IsTakeTest || appointment.ISLocked);

            if (IsTakeTest)
            {
                int testID = appointment.GetTestID();
                if(testID != -1) 
                    lblShowTestID.Text = testID.ToString();
                return;
            }

            if(appointment.ISLocked)
            {
                lblShowError.Text = "Person already sat for the test, appointment loacked";
                return;
            }

            dateTimePicker1.Value = appointment.AppointmentDate.Date;     
        }

        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now.Date;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            frmAddNewAppointmentcs.AddNewAppointmentcs += _returnAppoimentDate;
        }
    }
}
