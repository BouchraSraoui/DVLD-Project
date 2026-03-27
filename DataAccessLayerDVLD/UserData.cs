using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessLayerDVLD
{
    public class UserData:clsDataBase
    {
        public static DataTable GetAllUsers()
        {
            string query = @"SELECT UserID, 
                                    UserName, 
                                    Users.PersonID,
                                    FirstName +' ' + LastName as FullName, 
                                    IsActive
                             FROM Users
                             INNER JOIN People
                             ON Users.PersonID =People.PersonID;";

            return ExectuteAdapter(query, null);
        }


        public static DataTable Filter(string Filter, object Field)
        {
            string query = @"SELECT UserID, 
                                    Users.PersonID,
                                    UserName, 
                                    FirstName +' ' + LastName as FullName, 
                                    IsActive
                             FROM Users
                             INNER JOIN People
                             ON Users.PersonID =People.PersonID
                             WHERE Users." + Filter + " = @Field";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Field",Field }

            };
            return ExectuteAdapter(query, Parameters);
        }


        public static bool GetUser(string UserName, string Password,
                                 ref int UserId, ref int PersonID,
                                 ref bool ISActive)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Users 
                             WHERE UserName = @UserName AND Password = @Password";

             SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@UserName", UserName);

            try
            {
              
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    UserId = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    ISActive = Convert.ToBoolean(reader["IsActive"]);

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

        public static object IsUser(int PersonID)
        {
            string query = @"SELECT CASE
                                        WHEN EXISTS (
                                                        SELECT 1 FROM Users
                                                        WHERE PersonID = @PersonID)
                                     THEN 1 ELSE 0 
                                      END AS HasApplication;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    {"@PersonID", PersonID}
            };
            return ExecuteScalar(query, parameters);
        }


        public static bool GetUserByID(int UserID, ref string UserName,
                                       ref string Password, ref int PersonID,
                                       ref bool ISActive)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Users 
                             WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@UserID", UserID);
           

            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    ISActive = Convert.ToBoolean(reader["IsActive"]);

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

        public static object FindUserByUserName(string UserName)
        {
            string query = @"SELECT UserID FROM Users 
                             WHERE UserName = @UserName";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@UserName",UserName }
            };
            return ExecuteScalar(query, parameters);
        }
        public static int AddNewUser(int PersonID, string UserName , string Password, bool ISActive)
        {
            string queru = @"INSERT INTO Users
                             VALUES( 
                                    @PersonID,
                                    @UserName,
                                    @Password,
                                    @ISActive);
                            SELECT SCOPE_IDENTITY()";
            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Password", Password },
                {"@PersonID",PersonID },
                {"@UserName", UserName },
                {"@ISActive",ISActive }
            };

            object UserID = ExecuteScalar(queru, Parameters);

            return UserID == null ? -1 : Convert.ToInt32(UserID);
        }

        public static bool UpdateUserInfo(int UserID, int PersonID, string UserName, string Password, bool ISActive)
        {
            string queru = @"UPDATE Users
                             SET 
                                Password = @Password,
                                PersonID=@PersonID,
                                UserName=@UserName,
                                IsActive =@ISActive
                             WHERE UserID = @UserID";
            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Password", Password },
                {"@UserID",UserID },
                {"@PersonID", PersonID },
                {"@UserName",UserName },
                {"@ISActive",ISActive }
            };

            return ExecuteNonQuery(queru, Parameters);
        }

       public static object UserHasData(int UserID)
        {
            string query = @"SELECT  CASE WHEN 
                                             @UserID IN (
                                                SELECT CreatedByUserID FROM Applications
                                                UNION
                                                SELECT CreatedByUserID FROM InternationalLicenses
                                                UNION
                                                SELECT CreatedByUserID FROM Licenses
                                                UNION
                                                SELECT CreatedByUserID FROM DetainedLicenses
                                                UNION
                                                SELECT CreatedByUserID FROM Drivers
                                                UNION
                                                SELECT CreatedByUserID FROM Tests
                                                UNION
                                                SELECT CreatedByUserID FROM TestAppointments
                                             )
                              THEN 1 ELSE 0 END AS HasData;";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                 {"@UserID",UserID }
            };

            return ExecuteScalar(query, Parameters);
        }

        public static bool Delete(int UserID)
        {
            string query = @"DELETE FROM Users
                             WHERE UserID =@UserID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@UserID",UserID }

            };
            return ExecuteNonQuery(query, Parameters);
        }

        public static int ISExist(int UserID)
        {
            string query = @"SELECT UserID FROM Users
                             WHERE UserID =@UserID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@UserID",UserID }

            };

            object UserId = ExecuteScalar(query, Parameters);

            if (UserId != null)
                return (int)UserId;
            return -1;
        }
    }

}
