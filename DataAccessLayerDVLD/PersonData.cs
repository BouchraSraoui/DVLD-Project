using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;
namespace DataAccessLayerDVLD
{
    public class PersonData : clsDataBase
    {

        public static DataTable GetAllPeople()
        {
            string query = @"SELECT PersonID,
                                   NationalNo,
                                   FirstName,
                                   SecondName,
                                   ThirdName,
                                   LastName,
                                   Gendor,
                                   DateOfBirth,
                                   CountryName AS Nationality,
                                   Address,
                                   Phone,
                                   Email
                            FROM People
                            INNER JOIN Countries
                            ON Countries.CountryID = People.NationalityCountryID";
            return ExectuteAdapter(query, null);
        }

        public static DataTable Filter(string Filter, object Field)
        {
            string query = @"SELECT PersonID,
                                   NationalNo,
                                   FirstName,
                                   SecondName,
                                   ThirdName,
                                   LastName,
                                   Gendor,
                                   DateOfBirth,
                                   CountryName as Nationality,
                                   Address,
                                   Phone,
                                   Email
                            FROM People
                            INNER JOIN Countries
                            ON Countries.CountryID = People.NationalityCountryID 
                            WHERE " + Filter + " = @Field";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@Field",Field }

            };
            return ExectuteAdapter(query, Parameters);
        }

        public static bool DeletePerson(int PersonID)
        {
            string query = @"DELETE FROM People
                             WHERE PersonID =@PersonID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@PersonID",PersonID }

            };
            return ExecuteNonQuery(query, Parameters);
        }

        public static bool FindPersonByID(
                                          int PersonId,
                                          ref string NationalNo,
                                          ref string FirstName,
                                          ref string SecondName,
                                          ref string ThirdName,
                                          ref string LastName,
                                          ref DateTime DateOfBirth,
                                          ref string Gendor,
                                          ref string Address,
                                          ref string Phone,
                                          ref string Email,
                                          ref int NationalityCountryID,
                                          ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonId);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();

                    SecondName = reader["SecondName"] != DBNull.Value ? reader["SecondName"].ToString() : "";
                    ThirdName = reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : "";
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";

                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = reader["Gendor"].ToString();
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

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


        public static int IsPersonExist(string NationalNo)
        {
            string query = @"SELECT PersonID FROM People
                             WHERE NationalNo =@NationalNo";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                {"@NationalNo",NationalNo }

            };

            object PersonID = ExecuteScalar(query, Parameters);

            if (PersonID != null)
                return (int)PersonID;
            return -1;
        }

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                                       DateTime DateOfBirth, string Gendor, string Address,
                                       string PhoneNumber, string Email, int CountryId, string ImagePath)
        {
            string query = @"
                               INSERT INTO People 
                               VALUES 
                                    ( @NationalNo,
                                      @FirstName, 
                                      @SecondName,
                                      @ThirdName, 
                                      @LastName, 
                                      @DateOfBirth, 
                                      @Gendor,
                                      @Address, 
                                      @Phone, 
                                      @Email, 
                                      @CountryId, 
                                      @ImagePath);
                               SELECT SCOPE_IDENTITY(); ";


            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@NationalNo", NationalNo},
                    {"@FirstName", FirstName},
                    {"@SecondName", SecondName},
                    {"@ThirdName", ThirdName},
                    {"@LastName", LastName},
                    {"@DateOfBirth", DateOfBirth},
                    {"@Gendor", Gendor},
                    {"@Address", Address},
                    {"@Phone", PhoneNumber},
                    {"@Email", Email},
                    {"@CountryId", CountryId},
                    {"@ImagePath", ImagePath}
                };

            object result = ExecuteScalar(query, parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return -1;
        }

        public static bool UpdatePersonInfo(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                                      DateTime DateOfBirth, string Gendor, string Address,
                                      string PhoneNumber, string Email, int CountryId, string ImagePath)
        {
            string query = @"
                             UPDATE People
                             SET 
                                NationalNo = @NationalNo,
                                FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName,
                                DateOfBirth = @DateOfBirth,
                                Gendor = @Gendor,
                                Address = @Address,
                                Phone = @Phone,
                                Email = @Email,
                                NationalityCountryID = @CountryId,
                                ImagePath = @ImagePath
                             WHERE PersonID = @PersonID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                  {"@PersonID", PersonID},
                  {"@NationalNo", NationalNo},
                  {"@FirstName", FirstName},
                  {"@SecondName", SecondName},
                  {"@ThirdName", ThirdName},
                  {"@LastName", LastName},
                  {"@DateOfBirth", DateOfBirth},
                  {"@Gendor", Gendor},
                  {"@Address", Address},
                  {"@Phone", PhoneNumber},
                  {"@Email", Email},
                  {"@CountryId", CountryId},
                  {"@ImagePath", ImagePath}
            };
            return ExecuteNonQuery(query, parameters);
        }

    }
}

