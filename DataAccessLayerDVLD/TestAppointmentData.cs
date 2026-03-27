using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessLayerDVLD
{
    public class TestAppointmentData : clsDataBase
    {
        public static int AddNewTestAppointment(int TestTypeID,
                                                int LDLAppID,
                                                DateTime AppointmentDate,
                                                double PaidFees,
                                                int UserID,
                                                bool IsLocked,
                                                object RetakeTestApplicationID)
        {
            string query = @"  INSERT INTO TestAppointments 
                               VALUES 
                                    ( @TestTypeID,
                                      @LDLAppID,
                                      @AppointmentDate,
                                      @PaidFees,
                                      @UserID,
                                      @IsLocked,
                                      @RetakeTestApplicationID);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@TestTypeID", TestTypeID},
                {"@LDLAppID", LDLAppID},
                {"@AppointmentDate" ,AppointmentDate},
                {"@PaidFees",PaidFees },
                {"@UserID",UserID },
                {"@IsLocked",IsLocked }, 
                {"@RetakeTestApplicationID",(int)RetakeTestApplicationID==-1 ?"" : RetakeTestApplicationID}
            };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;
        }

        public static bool UpdateTestAppointment(int AppintmentID,
                                                int TestTypeID,
                                                int LDLAppID,
                                                DateTime AppointmentDate,
                                                double PaidFees,
                                                int UserID,
                                                bool IsLocked)
        {
            string query = @"  Update  TestAppointments 
                               SET 
                                   TestTypeID = @TestTypeID,
                                   LocalDrivingLicenseApplicationID = @LDLAppID,
                                   AppointmentDate = @AppointmentDate,
                                   PaidFees = @PaidFees,
                                   CreatedByUserID = @UserID,
                                   IsLocked = @IsLocked
                               WHERE TestAppointmentID = @AppintmentID ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@TestTypeID", TestTypeID},
                    {"@LDLAppID", LDLAppID},
                    {"@AppointmentDate" ,AppointmentDate},
                    {"@PaidFees",PaidFees },
                    {"@UserID",UserID },
                    {"@IsLocked",IsLocked },
                    {"@AppintmentID",AppintmentID }
                };

            return ExecuteNonQuery(query, parameters);
        }

        public static bool GetTestAppointment(int TestAppointment,
                                              ref int testTypeID,
                                              ref int lDLAppID,
                                              ref DateTime appointmentDate,
                                              ref double paidFees,
                                              ref int createdByUserID,
                                              ref bool iSLocked,
                                              ref object RetakeTestApplicationID)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointment);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    testTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    lDLAppID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    appointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    paidFees = Convert.ToInt64(reader["PaidFees"]);
                    iSLocked = Convert.ToBoolean(reader["IsLocked"]);
                    RetakeTestApplicationID = reader["RetakeTestApplicationID"];
                                            
                    return true;
                }
                reader.Close();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return false;
        }


        public static DataTable GetAllAppointments(int LDLDApplicationID, int TestTypeID)
        {

            string query = @"SELECT TestAppointmentID,
                                    AppointmentDate,
                                    PaidFees,
                                    IsLocked
                            FROM TestAppointments 
                            WHERE LocalDrivingLicenseApplicationID = @LDLDApplicationID 
                                  AND 
                                  TestTypeID = @TestTypeID";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@LDLDApplicationID",LDLDApplicationID },
                {"@TestTypeID",TestTypeID }
            };
            return ExectuteAdapter(query, parameters);
        }
            

        public static bool Locked(bool IsLocked, int TestAppointmentID)
        {
            string queru = @"UPDATE TestAppointments
                             SET 
                                IsLocked = @IsLocked
                             WHERE TestAppointmentID = @TestAppointmentID";
            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@IsLocked", IsLocked },
                {"@TestAppointmentID",TestAppointmentID }
            };

            return ExecuteNonQuery(queru, Parameters);
        }


        public static int GetTestID(int TestAppointmentID)
        {
            string query = @"SELECT TestID FROM Tests
                            WHERE TestAppointmentID = @TestAppointmentID";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@TestAppointmentID",TestAppointmentID }
            };

            object TestID = ExecuteScalar(query, parameters);   

            return TestID==null?-1:Convert.ToInt32(TestID);

            
        }
    }
}
