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
    public partial class frmTestAppointment : Form
    {
        public delegate clsTest GetTestInfo();
        public static event GetTestInfo GetTest;

        clsTestType _TestType = null;
        clsLocalDrivingLicenseApplication _LDLAPP = null;
        public frmTestAppointment(clsLocalDrivingLicenseApplication LDLAPP , clsTestType TestType)
        {
            InitializeComponent();
            _LDLAPP = LDLAPP;
            _TestType = TestType;
           
        }

        private void _InitializeFrom()
        {
            ctrlShowApplicationDetails1.ShowLDLApplicationDetails(_LDLAPP);
            _ListAllAppointments();
        }

        private void _VisibiltyOfAddButton()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

                bool AddNewAppointment =
                                         ((bool)lastRow.Cells[3].Value
                                         && GetTest?.Invoke().TestResult == false)
                                         &&
                                         ((DateTime)lastRow.Cells[1].Value < DateTime.Now);

                btnAdd.Visible = AddNewAppointment;
            }
        }

        private void _ShowAddUpdateAppointmentsForm(clsTestAppointments Appointment)
        {
            frmAddNewAppointmentcs frmAddNewAppointmentcs = new frmAddNewAppointmentcs(Appointment,
                                                                                      _Trails());
            frmAddNewAppointmentcs.ShowDialog();


            _ListAllAppointments();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            clsTestAppointments appointment = new clsTestAppointments(_TestType, _LDLAPP, clsSessionInfo.User);
            _ShowAddUpdateAppointmentsForm(appointment);


        }

        private clsTestAppointments _GetAppointment()
        {
            int AppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            return clsTestAppointments.GetTestAppointment(AppointmentID);
        }

        private void _ListAllAppointments()
        {
          
           DataTable dtTestAppointments = clsTestAppointments.GetAllAppointments(_LDLAPP.LocalDrivingLicenseApplicationID,
                                                                                  _TestType.TestTypeID);

           dataGridView1.DataSource = dtTestAppointments;
            label4.Text=dataGridView1.RowCount.ToString();

            _VisibiltyOfAddButton();
        }

        private int _Trails()
        {
            int Trail = 0;

            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                if ((bool)Row.Cells[3].Value == true)
                    Trail++;
            }

            return Trail;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVisionTestcs_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _TestType.TestTypeTitle + " Appointment";
            _InitializeFrom();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frmTakeVisiontest = new frmScheduleTest(_GetAppointment(), _Trails());
            frmTakeVisiontest.ShowDialog();
            
            _InitializeFrom();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Items[1].Enabled = ((bool)dataGridView1.CurrentRow.Cells[3].Value != true);
        }

        private void ctrlShowApplicationDetails1_Load(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            clsTestAppointments appointment = clsTestAppointments.GetTestAppointment(AppointmentID);

            _ShowAddUpdateAppointmentsForm(appointment);

        }
    }
}
