using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayerDVLD;
namespace BusinessLayerDVLD
{
    public class clsCountry
    {

        public int CountryId {  get; set; }
        public string CountryName { get; set; }

        public clsCountry(int ID ,  string countryName)
        {
            CountryId = ID;
            CountryName = countryName;
        }
        public static DataTable GetAllCountries()
        {
            return CountryData.GetAllCountries();
        }
        public static string GetCountryName(int CountryID)
        {
            return CountryData.GetCountryName(CountryID);
        }
        public static int GetCountryID(string CountryName)
        {
            return CountryData.GetCountryID(CountryName);
        }
    }
}
