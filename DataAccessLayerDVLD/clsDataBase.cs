using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerDVLD
{
    public class clsDataBase: DataAccessLayerSettings
    {
        private static void _FillCommand(SqlCommand cmd, Dictionary<string, object> Parameters)
        {
            if (Parameters == null)
                return;

            foreach (var param in Parameters)
            {
                if(param.Value.ToString() =="")
                    cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                else
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
        }

        public static DataTable ExectuteAdapter(string query , Dictionary<string, object> Parameters)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(query, Connection);

            _FillCommand(command, Parameters);
            DataTable dt = new DataTable();

            try
            {
                Connection.Open();

                SqlDataAdapter result = new SqlDataAdapter(command);
                    result.Fill(dt);
                    return dt;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return null;

        }

        public static object ExecuteScalar(string query , Dictionary<string, object> Parameters)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(query, Connection);

             _FillCommand(command, Parameters);
            try
            {
                Connection.Open();

                object result = command.ExecuteScalar();
                return result;
            }
            catch
            {

            }
            finally
            {
                Connection.Close() ;
            }
            return null;
        }

        public static bool ExecuteNonQuery(string query, Dictionary<string, object> Parameters)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(query, Connection);

            _FillCommand(command, Parameters);
         

            try
            {
                Connection.Open();

                object result = command.ExecuteNonQuery();

                if (result != null )
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return false;
        }

    }
}
