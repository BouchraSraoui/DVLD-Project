using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerDVLD
{
    public class TestTypeData:clsDataBase
    {
        public static DataTable GetAllTestTypes()
        {
            return ExectuteAdapter("SELECT * FROM TestTypes;", null);
        }

        public static bool GetTestType(int TestTypeID,
                                       ref string TestTypeTitle,
                                       ref string TestTypeDescription,
                                       ref double TestTypeFes)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM TestTypes 
                             WHERE TestTypeID = @TestTypeID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeTitle = reader["TestTypeTitle"].ToString();
                    TestTypeFes = Convert.ToInt64(reader["TestTypeFees"]);
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
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

        public static bool UpdateTestTypeInfo(int TestTypeID,
                                             string TestTypeTitle,
                                             string TestTypeDescription,
                                             double TestTypeFees)
        {
            string query = @"
                             UPDATE TestTypes
                             SET 
                                TestTypeTitle = @TestTypeTitle,
                                TestTypeFees = @TestTypeFees,
                                TestTypeDescription=@TestTypeDescription
                             WHERE TestTypeID = @TestTypeID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                  {"@TestTypeTitle", TestTypeTitle},
                  {"@TestTypeDescription", TestTypeDescription},
                  {"@TestTypeFees", TestTypeFees},
                  {"@TestTypeID", TestTypeID}
            };
            return ExecuteNonQuery(query, parameters);
        }

    }
}
