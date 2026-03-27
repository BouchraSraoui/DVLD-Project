using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerDVLD
{
    public class DetainLicenseData:clsDataBase
    {
        public static int AddNewDetainedLicense(int LicenseID,
                                        DateTime DetainDate,
                                        double FineFees,
                                        int CreatedByUserID)
        {                               

            string query = @" INSERT INTO DetainedLicenses( LicenseID, DetainDate, FineFees, CreatedByUserID) 
                               VALUES 
                                    ( @LicenseID,
                                      @DetainDate,
                                      @FineFees,
                                      @CreatedByUserID);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    {"@LicenseID", LicenseID},
                    {"@DetainDate", DetainDate},
                    {"@FineFees", FineFees },
                    {"@CreatedByUserID", CreatedByUserID}
                };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;

        }



        public static bool GetDetainLicenseInfo(int LicenseID,
                                      ref int DetainID,
                                      ref DateTime DetainDate,
                                      ref double FineFees,
                                      ref int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = @"SELECT DetainID,
                                    DetainDate,
                                    FineFees,
                                    CreatedByUserID
                             FROM DetainedLicenses 
                             WHERE LicenseID = @LicenseID AND IsReleased = 0";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            SqlDataReader reader = null;

            try
            {

                connection.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToInt64(reader["FineFees"]);

                    return true;
                }

            }

            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
                reader.Close();
            }
            return false;

        }


        public static bool ReleaseLicense(int DetainLicenseID,
                                     bool IsReleased,
                                     DateTime ReleaseDate,
                                     int ReleaseByUserID,
                                     int ReleaseApplicationID)
        {

            string query = @" UPDATE DetainedLicenses
                               SET 
                                  IsReleased = @IsReleased,
                                  ReleaseDate = @ReleaseDate,
                                  ReleasedByUserID = @ReleaseByUserID,
                                  ReleaseApplicationID = @ReleaseApplicationID

                               WHERE DetainID =@DetainLicenseID ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@IsReleased", IsReleased},
                {"@ReleaseDate", ReleaseDate},
                {"@DetainLicenseID", DetainLicenseID },
                {"@ReleaseByUserID", ReleaseByUserID},
                { "@ReleaseApplicationID", ReleaseApplicationID}
                };
            return ExecuteNonQuery(query, parameters);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            string query = @"SELECT  
                                    DetainID, 
                                   DL.LicenseID, 
                                   DetainDate,
                                   FineFees, 
                                   IsReleased , 
                                   ReleaseDate,
                                   NationalNo AS NNO, 
                                   FirstName +' ' +LastName AS FullName,
                                   ReleaseApplicationID
                             FROM DetainedLicenses AS DL
                             INNER JOIN (SELECT LicenseID, ApplicantPersonID FROM Licenses 
                                         INNER JOIN Applications 
                                         ON Licenses.ApplicationID = Applications.ApplicationID)AS LP
                             ON DL.LicenseID = LP.LicenseID
                             INNER JOIN People
                             ON LP.ApplicantPersonID = People.PersonID; ";

            return ExectuteAdapter(query, null);
        }


        public static DataTable Filter(string Filter, object Field)
        {

            if (Filter == "LicenseID")
                Filter = "DL.LicenseID";

            string query = @"SELECT  
                                   DetainID, 
                                   DL.LicenseID, 
                                   DetainDate,
                                   FineFees, 
                                   IsReleased , 
                                   ReleaseDate,
                                   NationalNo AS NNO, 
                                   FirstName +' ' +LastName AS FullName,
                                   ReleaseApplicationID
                             FROM DetainedLicenses AS DL
                             INNER JOIN (SELECT LicenseID, ApplicantPersonID FROM Licenses 
                                         INNER JOIN Applications 
                                         ON Licenses.ApplicationID = Applications.ApplicationID)AS LP
                             ON DL.LicenseID = LP.LicenseID
                             INNER JOIN People
                             ON LP.ApplicantPersonID = People.PersonID
                             WHERE " + Filter + "= @Field";
  
            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Field",Field }
            };

            return ExectuteAdapter(query, Parameters);
        }
    }
}
