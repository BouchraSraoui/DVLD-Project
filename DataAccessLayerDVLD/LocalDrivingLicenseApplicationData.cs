using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayerDVLD
{
    public class LocalDrivingLicenseApplicationData:clsDataBase
    {


        public static bool FindLDApp(int LDLAppID,
                                     ref int ApplicationID,
                                     ref int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = @"
                            SELECT ApplicationID, 
                                   LicenseClassID
                            FROM LocalDrivingLicenseApplications
                            WHERE LocalDrivingLicenseApplicationID = @LDLAppID;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LDLAppID", LDLAppID);
           
            try
            { 
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                    reader.Close();
                    return true;
                    
                }
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
    
        public static DataTable GetLDLAppsTestResults()
    {
        string query = @"
                         SELECT LDLApp.LocalDrivingLicenseApplicationID,
                                LicenseClasses.ClassName,
                                People.FirstName +' ' +
                                                       IsNULL(SecondName,'') + 
                                                       IsNULL(ThirdName,'') +' '+
                                                       People.LastName AS FullName,
                                People.NationalNo,
                                Applications.ApplicationDate,
                                LD.PassedTests,
                                CASE 
                                    WHEN Applications.ApplicationStatus = 1 THEN 'New' 
                                    WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' 
                                    WHEN Applications.ApplicationStatus = 3 THEN 'Completed' 
                                    END AS Status

                         FROM LocalDrivingLicenseApplications AS LDLApp
                         INNER JOIN LicenseClasses
                         ON LDLApp.LicenseClassID = LicenseClasses.LicenseClassID
                         INNER JOIN Applications 
                         ON LDLApp.ApplicationID = Applications.ApplicationID
                         INNER JOIN People
                         ON People.PersonID = Applications.ApplicantPersonID
        
                         INNER JOIN 
                         (SELECT COUNT(CASE WHEN Tests.TestResult = 1 THEN 1 END) AS PassedTests ,LocalDrivingLicenseApplications .LocalDrivingLicenseApplicationID FROM 
                                 LocalDrivingLicenseApplications 
                         LEFT JOIN TestAppointments
                         ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                         LEFT JOIN Tests
                         ON  Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                         GROUP BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                         )AS LD
                         ON LDLApp.LocalDrivingLicenseApplicationID = LD.LocalDrivingLicenseApplicationID;
                         ";

            return ExectuteAdapter(query, null); ;
    }

        public static bool FindLDAppWithTestResult( int LDLAppID,
                                                 ref int ApplicationID,
                                                 ref int LicenseClassID,
                                                 ref int PassedTests)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"
                            SELECT 
                               LDLApp.LicenseClassID,
                               LDLApp.ApplicationID,
                               LD.PassedTests
                           FROM LocalDrivingLicenseApplications AS LDLApp
                           LEFT JOIN 
                           (
                           SELECT 
                               LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,
                               COUNT(CASE WHEN Tests.TestResult = 1 THEN 1 END) AS PassedTests
                           FROM 
                               LocalDrivingLicenseApplications
                           LEFT JOIN TestAppointments
                               ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                           LEFT JOIN Tests
                               ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                           GROUP BY 
                               LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                           ) AS LD
                               ON LDLApp.LocalDrivingLicenseApplicationID = LD.LocalDrivingLicenseApplicationID
                           WHERE 
                           LDLApp.LocalDrivingLicenseApplicationID = @LDLAppID ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    PassedTests = Convert.ToInt32(reader["PassedTests"]);
           

                    reader.Close();
                    return true;
                }

                
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

        public static int GetLicenseClassID(int AppID)
        {
            string query = @"SELECT LicenseClassID FROM LocalDrivingLicenseApplications
                            WHERE ApplicationID = @AppID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            { { "@AppID", AppID } };

            object LicenseClassID  = ExecuteScalar(query, Parameters);
            if (LicenseClassID != null)
                return (int)LicenseClassID;
            return -1;

        }

        public static int AddNewLDLApplication(int ApplicationID, int LicenseClassID)
        {

            string query = @"
                               INSERT INTO LocalDrivingLicenseApplications 
                               VALUES 
                                    ( @ApplicationID,
                                      @LicenseClassID);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@ApplicationID", ApplicationID},
                    {"@LicenseClassID", LicenseClassID}
                };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;

        }

        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            string query = @"
                               UPDATE LocalDrivingLicenseApplications 
                               SET 
                                   LicenseClassID = @LicenseClassID
                               WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID; ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    {"@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID},
                    {"@LicenseClassID", LicenseClassID}
                };

            return ExecuteNonQuery(query, parameters);

        }

        public static DataTable Filter(string Filter , object Field)
        {
            string query = @"
                         SELECT LDLApp.LocalDrivingLicenseApplicationID,
                                LicenseClasses.ClassName,
                                People.FirstName +' ' +
                                                       IsNULL(SecondName,'') + 
                                                       IsNULL(ThirdName,'') +' '+
                                                       People.LastName AS FullName,
                                People.NationalNo,
                                Applications.ApplicationDate,
                                LD.PassedTests,
                                CASE 
                                    WHEN Applications.ApplicationStatus = 1 THEN 'New' 
                                    WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' 
                                    WHEN Applications.ApplicationStatus = 3 THEN 'Completed' 
                                    END AS Status

                         FROM LocalDrivingLicenseApplications AS LDLApp
                         INNER JOIN LicenseClasses
                         ON LDLApp.LicenseClassID = LicenseClasses.LicenseClassID
                         INNER JOIN Applications 
                         ON LDLApp.ApplicationID = Applications.ApplicationID
                         INNER JOIN People
                         ON People.PersonID = Applications.ApplicantPersonID
        
                         INNER JOIN 
                         (SELECT COUNT(CASE WHEN Tests.TestResult = 1 THEN 1 END) AS PassedTests ,LocalDrivingLicenseApplications .LocalDrivingLicenseApplicationID FROM 
                                 LocalDrivingLicenseApplications 
                         LEFT JOIN TestAppointments
                         ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                         LEFT JOIN Tests
                         ON  Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                         GROUP BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                         )AS LD
                         ON LDLApp.LocalDrivingLicenseApplicationID = LD.LocalDrivingLicenseApplicationID";

            if (Filter == "NationalNo")
                query += " WHERE People.NationalNo = @Field";
            else if (Filter == "Status")
                query += " WHERE Applications.ApplicationStatus = @Field";
            else if (Filter == "L_DL_APPID")
                query += " WHERE LDLApp.LocalDrivingLicenseApplicationID = @Field";


            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Field" ,Field}
            };
            return ExectuteAdapter(query, Parameters); ;
        
    }

    }
}
