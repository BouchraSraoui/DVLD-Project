using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace BusinessLayerDVLD
{
    public class clsTest
    {
        enum enMode
        {
            Add,
            Update
        }
        enMode _Mode = enMode.Add;
        public int TestID {  get; set; }
        public clsTestAppointments TestAppointment {  get; set; }
        public bool TestResult {  get; set; }
        public string Notes {  get; set; }
        public clsUser CreatedByUser { get; set; }


        public clsTest(clsTestAppointments TestAppointment,  clsUser CreatedByUser)
        {
            this.TestAppointment = TestAppointment;
            this.CreatedByUser = CreatedByUser;
            Notes = "";
            TestResult = false;

            _Mode = enMode.Add;
        }
        public clsTest(int testID, 
            int testAppointmentID,
            bool testResult, 
            string notes, 
            int createdByUserID)
        {
            TestID = testID;
            TestAppointment = clsTestAppointments.GetTestAppointment(testAppointmentID);
            TestResult = testResult;
            Notes = notes;
            CreatedByUser = clsUser.GetUserByID(createdByUserID);

            _Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {

            TestID = TestData.AddNewTest(TestAppointment.TestAppointmentID,
                                       TestResult,
                                       Notes,
                                       CreatedByUser.UserID);
            return TestID != -1;
        }


        private bool _UpdateTestInfo()
        {

            return TestData.UpdateTestInfo(TestID,
                                          TestAppointment.TestAppointmentID,
                                          TestResult,
                                          Notes,
                                          CreatedByUser.UserID);
        }
        public bool Save()
        {
            if (_Mode == enMode.Add)
                return _AddNewTest();
            return _UpdateTestInfo();
        }
    }
}
