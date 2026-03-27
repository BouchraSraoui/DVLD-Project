using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerDVLD
{
    public class TestData:clsDataBase
    {
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int UserID)
        {
            string queru = @"INSERT INTO Tests
                             VALUES( 
                                    @TestAppointmentID,
                                    @TestResult,
                                    @Notes,
                                    @UserID);
                            SELECT SCOPE_IDENTITY()";
            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@TestAppointmentID", TestAppointmentID },
                {"@TestResult",TestResult },
                {"@Notes", Notes },
                {"@UserID",UserID }
            };

            object TestID = ExecuteScalar(queru, Parameters);

            return TestID == null ? -1 : Convert.ToInt32(TestID);
        }


        public static bool UpdateTestInfo(int TestID, int TestAppointmentID, bool TestResult, string Notes, int UserID)
        {
            string queru = @"UPDATE Tests
                             SET 
                                TestAppointmentID = @TestAppointmentID,
                                PersonID=@PersonID,
                                UserName=@UserName,
                                IsActive =@ISActive
                             WHERE TestID = @TestID";
            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@TestAppointmentID", TestAppointmentID },
                {"@TestID",TestID },
                {"@TestResult", TestResult },
                {"@Notes",Notes },
                {"@UserID",UserID }
            };

            return ExecuteNonQuery(queru, Parameters);
        }

    }
}
