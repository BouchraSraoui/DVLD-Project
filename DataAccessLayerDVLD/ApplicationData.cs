using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayerDVLD
{
    public class ApplicationData:clsDataBase
    {
        public static bool GetApplicationInfoByID(
                                          int ApplicationID,
                                          ref int PersonID,
                                          ref int ApplicationTypeID,
                                          ref int CreatedByUserID,
                                          ref DateTime ApplicationDate,
                                          ref DateTime LastStatusDate,
                                          ref int ApplicationStatus,
                                          ref double PaidFees)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Applications 
                             WHERE ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);

                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
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

        public static bool DeleteApplication(int ApplicationID)
        {
            string query = @"DELETE FROM Applications
                             WHERE ApplicationID =@ApplicationID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@ApplicationID",ApplicationID }

            };
            return ExecuteNonQuery(query, Parameters);
        }

        public static DataTable GetAppsRelatedPerson(int PersonID)
        {
            string query = @"SELECT * FROM Applications
                            WHERE ApplicantPersonID = @PersonID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            { {"@PersonID", PersonID } };
            return ExectuteAdapter(query, Parameters);
                
        }

        public static int AddNewApplication(int PersonID, int ApplicationTypeID, double  PaidFees, int UserID,
                                     DateTime ApplicationDate, DateTime LastStatusDate, int ApplicationStatus)                          
        {
            
                string query = @"
                               INSERT INTO Applications 
                               VALUES 
                                    ( @PersonID,
                                      @ApplicationDate, 
                                      @ApplicationTypeID,
                                      @ApplicationStatus, 
                                      @LastStatusDate, 
                                      @PaidFees, 
                                      @UserID);
                               SELECT SCOPE_IDENTITY(); ";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@PersonID", PersonID},
                    {"@ApplicationDate", ApplicationDate},
                    {"@ApplicationTypeID", ApplicationTypeID},
                    {"@ApplicationStatus", ApplicationStatus},
                    {"@LastStatusDate", LastStatusDate},
                    {"@PaidFees", PaidFees},
                    {"@UserID", UserID}
                };

                object result = ExecuteScalar(query, parameters);

                if (result != null)
                    return Convert.ToInt32(result);

                return -1;
            
        }

        public static bool UpdateApplicationInfo(int ApplicationID,int PersonID, int ApplicationTypeID, double PaidFees, int UserID,
                                     DateTime ApplicationDate, DateTime LastStatusDate, int ApplicationStatus)
        {
            string query = @"
                             UPDATE Applications
                             SET 
                                ApplicantPersonID = @PersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus,
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @UserID
                             WHERE ApplicationID = @ApplicationID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                  {"@PersonID", PersonID},
                  {"@ApplicationID", ApplicationID},
                  {"@ApplicationDate", ApplicationDate},
                  {"@ApplicationTypeID", ApplicationTypeID},
                  {"@ApplicationStatus", ApplicationStatus},
                  {"@LastStatusDate", LastStatusDate},
                  {"@PaidFees", PaidFees},
                  {"@UserID", UserID }
            };
            return ExecuteNonQuery(query, parameters);
        }

        public static object PersonHasApplication(int PersonID)
        {
            string query = @"SELECT CASE
                                        WHEN EXISTS (
                                                        SELECT 1 FROM Applications
                                                        WHERE ApplicantpersonID = @PersonID)
                                     THEN 1 ELSE 0 
                                      END AS HasApplication;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    {"@PersonID", PersonID}
            };
            return ExecuteScalar(query, parameters);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            string query = @"SELECT ActiveApplicationID=ApplicationID FROM Applications 
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                                  AND 
                                  ApplicationTypeID = @ApplicationTypeID 
                                  AND
                                  ApplicationStatus = 1";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@ApplicantPersonID", PersonID },
                {"@ApplicationTypeID", ApplicationTypeID }
            };
             object result = ExecuteScalar(query, Parameters);
            return (result != null) ? Convert.ToInt32(result) : -1;
        }

        public static int GetApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            string query = @"SELECT Applications.ApplicationID From Applications
                            INNER JOIN LocalDrivingLicenseApplications AS LDLApp
                            ON Applications.ApplicationID = LDLApp.ApplicationID
                            WHERE Applications.ApplicantPersonID = @PersonID 
                                  AND
                                  LDLApp.LicenseClassID = @LicenseClassID
                                  AND
                                  Applications.ApplicationTypeID = @ApplicationtypeID
                                  AND
                                  Applications.ApplicationStatus = 1";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@PersonID", PersonID },
                {"@LicenseClassID", LicenseClassID },
                {"@ApplicationTypeID", ApplicationTypeID }
            };
            object result = ExecuteScalar(query, Parameters);
            return (result != null) ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateStatus(int ApplicationID, int ApplicationStatus, DateTime LastStatusDate)
        {
            string query = @"
                             UPDATE Applications
                             SET 
                                ApplicationStatus = @ApplicationStatus,
                                LastStatusDate = @LastStatusDate,
                             WHERE ApplicationID = @ApplicationID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                  {"@ApplicationID", ApplicationID},
                  {"@ApplicationStatus", ApplicationStatus},
                  {"@LastStatusDate", LastStatusDate}
            };
            return ExecuteNonQuery(query, parameters);
        }
        }
    }
