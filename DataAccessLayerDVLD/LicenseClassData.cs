using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayerDVLD
{
    public class LicenseClassData:clsDataBase
    {
        public static bool GetLicenseTypeByID(
                                              int LicenseClassId,
                                              ref string ClassName,
                                              ref string Description,
                                              ref int MinimumAllowedAge,
                                              ref int DefaultValidityLength,
                                              ref double ClassFees)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = @"SELECT * FROM LicenseClasses
                             WHERE LicenseClassId = @LicenseClassId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LicenseClassId", LicenseClassId);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ClassName = reader["ClassName"].ToString();
                    Description = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToDouble(reader["ClassFees"]);

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

        public static DataTable GetAllLicenseClasses()
        {
            return ExectuteAdapter("SELECT * FROM LicenseClasses", null);
        }
    }
}
