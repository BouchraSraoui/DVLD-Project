using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DataAccessLayerDVLD
{
    public class ApplicationTypeData:clsDataBase
    {
        public static DataTable GetAllApplicationTypes()
        {
            return ExectuteAdapter( query:"SELECT * FROM ApplicationTypes", null);
        }

        public static bool GetApplicationInfoByIDType(int ApplicationTypeId,
                                              ref string ApplicationTypeTitle,
                                              ref double ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM ApplicationTypes 
                             WHERE ApplicationTypeID = @ApplicationTypeId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationTypeId", ApplicationTypeId);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = Convert.ToInt64(reader["ApplicationFees"]);

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


        public static bool UpdateAppTypeInfo(int ApplicationTypeId,
                                             string ApplicationTypeTitle,
                                             double ApplicationFees)
        {
            string query = @"
                             UPDATE ApplicationTypes
                             SET 
                                ApplicationTypeTitle = @ApplicationTypeTitle,
                                ApplicationFees = @ApplicationFees
                             WHERE ApplicationTypeID = @ApplicationTypeId;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                  {"@ApplicationFees", ApplicationFees},
                  {"@ApplicationTypeTitle", ApplicationTypeTitle},
                  {"@ApplicationTypeId", ApplicationTypeId}
            };
            return ExecuteNonQuery(query, parameters);
        }
    }
}
