using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DataAccessLayerDVLD
{
    public class LicenseData : clsDataBase
    {
        public static int GetLicenseIDByApplicationID(int appID)
        {
            string query = @"SELECT LicenseID FROM Licenses
                             WHERE ApplicationID = @appID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@appID",appID }
            };

            object LicenseID = ExecuteScalar(query, Parameters);

            if (LicenseID != null)
            {
                return (int)LicenseID;
            }
            return -1;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees,
            bool ISActive, int IssueReason, int CreatedByUserID)
        {

            string query = @"
                               INSERT INTO Licenses 
                               VALUES 
                                    ( @ApplicationID,
                                      @DriverID,
                                      @LicenseClassID,
                                      @IssueDate,
                                      @ExpirationDate,
                                      @Notes,
                                      @PaidFees,
                                      @IsActive,
                                      @IssueReason,
                                      @CreatedByUserID);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@ApplicationID", ApplicationID},
                    {"@LicenseClassID", LicenseClassID},
                    {"@DriverID", DriverID},
                    {"@IssueDate", IssueDate},
                    {"@ExpirationDate", ExpirationDate},
                    {"@Notes", Notes},
                    {"@PaidFees", PaidFees},
                    {"@IsActive", ISActive},
                    {"@CreatedByUserID", CreatedByUserID},
                    {"@IssueReason",IssueReason }
                };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;

        }

        public static bool UpdateLicenseInfo(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
           DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees,
           bool ISActive, int IssueReason, int CreatedByUserID)
        {

            string query = @"
                               UPDATE  Licenses 
                               SET 
                                   ApplicationID = @ApplicationID,
                                   DriverID = @DriverID,
                                   LicenseClassID = @LicenseClassID,
                                   IssueDate = @IssueDate,
                                   ExpirationDate = @ExpirationDate,
                                   Notes = @Notes,
                                   PaidFees = @PaidFees,
                                   IsActive = @IsActive,
                                   IssueReason = @IssueReason,
                                   CreatedByUserID = @CreatedByUserID
                                WHERE LicenseID = @LicenseID ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@ApplicationID", ApplicationID},
                    {"@LicenseClassID", LicenseClassID},
                    {"@DriverID", DriverID},
                    {"@IssueDate", IssueDate},
                    {"@ExpirationDate", ExpirationDate},
                    {"@Notes", Notes},
                    {"@PaidFees", PaidFees},
                    {"@IsActive", ISActive},
                    {"@CreatedByUserID", CreatedByUserID},
                    {"@IssueReason",IssueReason },
                    {"@LicenseID",LicenseID }
                };

            return ExecuteNonQuery(query, parameters);
        }

        public static bool DisactiveLicense(int LicenseID,
                                            bool ISActive)
        {

            string query = @"
                               UPDATE  Licenses 
                               SET 
                                   IsActive = @IsActive
       
                                WHERE LicenseID = @LicenseID ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                   
                    {"@IsActive", ISActive},
                    {"@LicenseID",LicenseID }
                };

            return ExecuteNonQuery(query, parameters);
        }

        public static int IsActive(int LicenseID)
        {
            string query = @"SELECT IsActive FROM Licenses
                             WHERE LicenseID = @LicenseID;";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@LicenseID",LicenseID }
            };

            object IsActive = ExecuteScalar(query, Parameters);
            return IsActive == null ? -1 : Convert.ToInt32(IsActive);
        }

        public static DataTable GetAllLicensesOfDriver(int DriverID)
        {
            string query = @"SELECT LicenseID,
                                    ApplicationID,
                                    DriverID,
                                    ClassName,
                                    IssueDate,
                                    ExpirationDate,
                                    Notes,
                                    PaidFees,
                                    IsActive,
                                    IssueReason,
                                    CreatedByUserID
                             FROM Licenses
                             INNER JOIN LicenseClasses
                             ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                             WHERE DriverID = @DriverID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@DriverID",DriverID }
            };
            return ExectuteAdapter(query, Parameters);
        }


        public static bool GetLicense(int LicenseID,
                                      ref int ApplicationID,
                                      ref int driverID,
                                      ref int licenseClassID,
                                      ref DateTime issueDate,
                                      ref DateTime expirationDate,
                                      ref string notes,
                                      ref double paidFees,
                                      ref bool iSActive,
                                      ref int issueReason,
                                      ref int createdByUserID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            SqlDataReader reader = null;

            try
            {

                connection.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    driverID = Convert.ToInt32(reader["DriverID"]);
                    notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                    licenseClassID = Convert.ToInt32(reader["LicenseClass"]);
                    issueReason = Convert.ToInt32(reader["IssueReason"]);
                    createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    issueDate = Convert.ToDateTime(reader["IssueDate"]);
                    expirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    iSActive = Convert.ToBoolean(reader["IsActive"]);                  
                    paidFees = Convert.ToInt64(reader["PaidFees"]);
                    
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

        public static object HasInternationalLicense(int LocalLicenseID)
        {
            string query = @"SELECT InternationalLicenseID FROM InternationalLicenses
                             WHERE IssuedUsingLocalLicenseID = @LicenseID AND IsActive = 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@LicenseID", LocalLicenseID }
            };
            return ExecuteScalar(query, parameters);
        }

        public static object IsDetained(int LicenseID)
        {
            string query = @"SELECT 
                                    CASE
                                     WHEN EXISTS(SELECT 1 FROM DetainedLicenses 
                                                  WHERE LicenseID = @LicenseID AND IsReleased = 0 )
                                     THEN 1 ELSE 0 END;";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@LicenseID",LicenseID }
            };

            return ExecuteScalar(query, Parameters);
        }


        public static int GetActiveLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            string query = @"SELECT LicenseID FROM Licenses
                             INNER JOIN Drivers
                             ON Drivers.DriverID = Licenses.DriverID
                             WHERE Drivers.PersonID = @PersonID AND LicenseClassID = @LicenseClassID AND IsActive = 1;";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@PersonID", PersonID },
                {"@LicenseClassID", LicenseClassID }
            };

            object result = ExecuteScalar(query, Parameters);

            return result != null ? Convert.ToInt32(result) : -1;


        }
    }
}