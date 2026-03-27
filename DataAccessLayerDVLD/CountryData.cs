using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerDVLD
{
    public class CountryData : clsDataBase
    {

        public static DataTable GetAllCountries()
        {
            return clsDataBase.ExectuteAdapter("SELECT * FROM Countries", null);
        }

        public static string GetCountryName(int CountryID)
        {
            string query = @"SELECT CountryName From Countries
                             WHERE CountryID = @CountryID";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                { "@CountryID", CountryID }
            };
            object CountryName = ExecuteScalar(query, Parameters);

            if (CountryName != null)
                return CountryName.ToString();
            return "";
        }

        public static int GetCountryID(string CountryName)
        {
            string query = @"SELECT CountryID From Countries
                             WHERE CountryName = @CountryName";

            Dictionary<string, object> Parameters = new Dictionary<string, object>
            {
                { "@CountryName", CountryName }
            };
            object CountryID = ExecuteScalar(query, Parameters);

            if (CountryID != null)
                return Convert.ToInt32(CountryID);
            return -1;
        }
    }
}
