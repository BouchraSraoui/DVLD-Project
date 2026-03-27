using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;
namespace BusinessLayerDVLD
{
    public class clsTestAppointments
    {
        public int TestAppointmentID {  get; set; }
        public clsTestType TestType { get; set; }
        public clsLocalDrivingLicenseApplication LDLApp {  get; set; }
        public DateTime AppointmentDate { get; set; }
        public double PaidFees {  get; set; }
        public clsUser CreatedByUser { get; set; }
        public bool ISLocked {  get; set; }
        public clsApplication RetakeTestApplication { get; set; }
        enum enMode
        {
            Add,
            Update
        }
        enMode _Mode = enMode.Add;
        public clsTestAppointments(clsTestType testType, 
                                   clsLocalDrivingLicenseApplication LDLApp,
                                   clsUser CreatedByUser)
        {
            TestType = testType;
            this.LDLApp = LDLApp;
            this.CreatedByUser = CreatedByUser;
            PaidFees = TestType.TestTypeFees;
            AppointmentDate = DateTime.Now;
            RetakeTestApplication = null;

            _Mode = enMode.Add;
        }


        public clsTestAppointments(int testAppointmentID, 
                                   int testTypeID, 
                                   int lDLAppID, 
                                   DateTime appointmentDate, 
                                   double paidFees, 
                                   int createdByUserID, 
                                   bool iSLocked,
                                   object RetakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestType = clsTestType.GetTestType(testTypeID);
            LDLApp = clsLocalDrivingLicenseApplication.FindLDApp(lDLAppID);
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUser = clsUser.GetUserByID(createdByUserID);
            ISLocked = iSLocked;
            RetakeTestApplication = RetakeTestApplicationID == DBNull.Value ?
                null :
                clsApplication.GetApplicationInfoByID(Convert.ToInt32(RetakeTestApplicationID));

            _Mode = enMode.Update;
        }

        private  bool _Update()
        {
            return TestAppointmentData.UpdateTestAppointment(TestAppointmentID,
                                                             TestType.TestTypeID,
                                                             LDLApp.LocalDrivingLicenseApplicationID,
                                                             AppointmentDate,
                                                             PaidFees,
                                                             CreatedByUser.UserID,
                                                             ISLocked);

          
        }

        private bool _Add()
        {

            
            TestAppointmentID = TestAppointmentData.AddNewTestAppointment(TestType.TestTypeID,
                                                                          LDLApp.LocalDrivingLicenseApplicationID,
                                                                          AppointmentDate,
                                                                          PaidFees,
                                                                          CreatedByUser.UserID,
                                                                          ISLocked,
                                                                          RetakeTestApplication==null ? -1 : RetakeTestApplication.ApplicationID);

            return TestAppointmentID != -1;
        }

        private void _TotalPaidFees()
        {
            if (RetakeTestApplication != null)
                PaidFees += RetakeTestApplication.PaidFees;
        }
        public bool Save()
        {
            _TotalPaidFees();

            if (_Mode == enMode.Update)
                return _Update();
            else
                return _Add();
        }

        public static clsTestAppointments GetTestAppointment(int TestAppointment)
        {
            int testTypeID = -1;
            int lDLAppID = -1;
            DateTime appointmentDate = DateTime.Now;
            double paidFees = 0;
            int createdByUserID = -1;
            bool iSLocked = true;
            object RetakeTestApplicationID = null;
            if (TestAppointmentData.GetTestAppointment(TestAppointment,
                                                       ref testTypeID,
                                                       ref lDLAppID,
                                                       ref appointmentDate,
                                                       ref paidFees,
                                                       ref createdByUserID,
                                                       ref iSLocked,
                                                       ref RetakeTestApplicationID))
                return new clsTestAppointments(TestAppointment,
                                               testTypeID,
                                               lDLAppID,
                                               appointmentDate,
                                               paidFees,
                                               createdByUserID,
                                               iSLocked,
                                               RetakeTestApplicationID);
            return null;
        }


        public static DataTable GetAllAppointments(int LDLApplicationID, int TestTypeID)
        {
            return TestAppointmentData.GetAllAppointments(LDLApplicationID, TestTypeID);
        }

        public bool Locke()
        {
            return TestAppointmentData.Locked(IsLocked:true, TestAppointmentID);
        }

        public int GetTestID()
        {
            return TestAppointmentData.GetTestID(TestAppointmentID);
        }

        
    }
}
