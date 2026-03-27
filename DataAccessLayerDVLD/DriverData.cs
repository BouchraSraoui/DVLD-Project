using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DataAccessLayerDVLD
{
    public class DriverData : clsDataBase
    {
        public static DataTable GetAllDrivers()
        {
            string query = @"SELECT DriverID,
                                  Drivers.PersonID,
                                  FirstName + ' ' +LastName As FullName,
                                  UserName,
                                  NationalNo,
                                  CreatedDate
                            FROM Drivers
                            INNER JOIN People 
                            ON Drivers.PersonID = People.PersonID
                            INNER JOIN Users
                            ON Users.UserID = Drivers.CreatedByUserID;";

            return ExectuteAdapter(query, null);
        }

        public static bool GetDriver(int DriverID, ref int PersonID,
                                      ref int CreatedByUserID,
                                      ref DateTime CreatedDate)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Drivers 
                             WHERE DriverID = @DriverID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@DriverID", DriverID);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

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

        public static int AddNewDriver(int PersonID, int UserID, DateTime CreatedDate)
        {
            string query = @"
                               INSERT INTO Drivers 
                               VALUES 
                                    ( @PersonID,
                                      @UserID, 
                                      @CreatedDate);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@PersonID", PersonID},
                    {"@UserID", UserID},
                    {"@CreatedDate", CreatedDate}
                };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;
        }

        public static bool GetDriverRelatedPerson(int PersonID, ref int DriverID,
                                      ref int CreatedByUserID,
                                      ref DateTime CreatedDate)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Drivers 
                             WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

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

        public static DataTable Filter(string Filter, object Field)
        {
            if (Filter == "PersonID")
                Filter = "Drivers.PersonID";

            string query = @"SELECT DriverID,
                                  Drivers.PersonID,
                                  FirstName + ' ' +LastName As FullName,
                                  UserName,
                                  NationalNo,
                                  CreatedDate
                            FROM Drivers
                            INNER JOIN People 
                            ON Drivers.PersonID = People.PersonID
                            INNER JOIN Users
                            ON Users.UserID = Drivers.CreatedByUserID
                            WHERE " + Filter + "= @Field";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Field",Field }
            };

            return ExectuteAdapter(query, Parameters);
        }

        public static object GetLicense_Class3_Driver(int DriverID)
        {
            string query = @"SELECT LicenseID FROM Licenses 
                            WHERE DriverID = @DriverID AND LicenseClass = 3 AND IsActive=1;" ;

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@DriverID", DriverID }
            };

            return ExecuteScalar(query, Parameters);
        }

        public static object Has_Active_International_License(int DriverID)
        {
            string query = @"SELECT InternationalLicenseID FROM InternationalLicenses 
                            WHERE DriverID = @DriverID AND IsActive=1;";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@DriverID", DriverID }
            };

            return ExecuteScalar(query, Parameters);
        }

        public static int IsDriver(int PersonId)
        {
            string query = @"SELECT DriverID FROM Drivers
                             WHERE PersonID = @PersonID";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@PersonID",PersonId }
            };

            object DriverId = ExecuteScalar(query, parameters);
            return DriverId == null ? -1 : Convert.ToInt32(DriverId);

        }

    }
}

