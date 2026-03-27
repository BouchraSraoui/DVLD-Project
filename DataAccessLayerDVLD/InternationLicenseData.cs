using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerDVLD
{
    public class InternationLicenseData:clsDataBase
    {

        public static DataTable GetAllIntLicenses()
        {
            string query = "SELECT * FROM InternationalLicenses;";
            return clsDataBase.ExectuteAdapter(query, null);
        }
        public static bool GetInterLicense(int LicenseID,
                                           ref int LocalLicenseID,
                                           ref int ApplicationId,
                                           ref DateTime IssueDate,
                                           ref DateTime ExpirationDate,
                                           ref bool IsActive,
                                           ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = @"SELECT * FROM InternationalLicenses 
                             WHERE InternationalLicenseID = @LicenseID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            SqlDataReader reader = null;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    LocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    ApplicationId = Convert.ToInt32(reader["ApplicationID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);

                    return true;
                }
                reader.Close();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return false;

        }



        public static DataTable GetInterLicenseOFLocalLicense(int LocalLicense)
        {
            string query = "SELECT * FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @LicenseID";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@LicenseID", LocalLicense }
            };
            return ExectuteAdapter(query, parameters);

        }


        public static int AddNewLicense(int ApplicationID,int DriverID, int LicenseID,
           DateTime IssueDate, DateTime ExpirationDate,
           bool ISActive, int CreatedByUserID)
        {

            string query = @"
                               INSERT INTO InternationalLicenses 
                               VALUES 
                                    ( @ApplicationID,
                                      @DriverID,
                                      @LicenseID, 
                                      @IssueDate,
                                      @ExpirationDate,
                                      @IsActive,
                                      @CreatedByUserID);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@ApplicationID", ApplicationID},
                    {"@LicenseID", LicenseID},
                    {"@IssueDate", IssueDate},
                    {"@ExpirationDate", ExpirationDate},
                    {"@IsActive", ISActive},
                    {"@CreatedByUserID", CreatedByUserID},
                    {"@DriverID", DriverID }
                };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;

        }

        public static bool UpdateLicenseInfo(int InterLicenseID, int ApplicationID, 
           int IssuedByLocalLicenseID,
           DateTime IssueDate, DateTime ExpirationDate,
           bool ISActive, int CreatedByUserID)
        {

            string query = @"
                               UPDATE  InternationalLicenses 
                               SET 
                                   ApplicationID = @ApplicationID,
                                   IssuedByLocalLicenseID = @IssuedByLocalLicenseID,
                                   IssueDate = @IssueDate,
                                   ExpirationDate = @ExpirationDate,
                                   IsActive = @IsActive,
                                   CreatedByUserID = @CreatedByUserID);
                                WHERE InernationalLicenseID = @InterLicenseID ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@ApplicationID", ApplicationID},
                    {"@IssuedByLocalLicenseID", IssuedByLocalLicenseID},
                    {"@InterLicenseID", InterLicenseID},
                    {"@IssueDate", IssueDate},
                    {"@ExpirationDate", ExpirationDate},
                    {"@IsActive", ISActive},
                    {"@CreatedByUserID", CreatedByUserID}
                };

            return ExecuteNonQuery(query, parameters);
        }


        public static bool DisactiveLicense(int InternationalLicenseID)
        {

            string query = @"
                               UPDATE  InternationalLicenses 
                               SET 
                                   IsActive = 0
       
                                WHERE InternationalLicenseID = @LicenseID ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@LicenseID",InternationalLicenseID }
                };

            return ExecuteNonQuery(query, parameters);
        }


        public static DataTable Filter (string Filter, object field)
        {
            string quey = @"SELECT * FROM InternationalLicenses
                            WHERE " + Filter + "=@field";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
                                                                {
                                                                    {"@field",field }
                                                                };

            return ExectuteAdapter(quey, Parameters);
        }
    }
}
