using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;

namespace BusinessLayerDVLD
{
    public class clsDriver
    {
        public int DriverID {  get; set; }
        public int PersonID {  get; set; }
        private clsPerson _Person {  get; set; }
        public clsPerson Person 
        { 
            get 
            {
                if (_Person == null && PersonID > 0)
                    _Person = clsPerson.FindPersonByID(PersonID);

                return _Person;
            } 
            set 
            { 
                Person = value;
                if (value != null)
                    PersonID = value.PersonID;
            } 
        }
        public int CreatedByUserID {  get; set; }
        private clsUser _CreatedByUser;
        public clsUser CreatedByUser
        {
            set
            {
                _CreatedByUser = value;
                if (value != null)
                    CreatedByUserID = value.UserID;
            }
            get
            {
                if (_CreatedByUser == null && CreatedByUserID >0)
                    _CreatedByUser = clsUser.GetUserByID(CreatedByUserID);
                return _CreatedByUser;
            }

        }
        public DateTime CreatedDate {  get; set; }

        public clsDriver(clsPerson Person, clsUser CreatedByUser)
        {
            this.Person = Person;
            this.CreatedByUser = CreatedByUser;
            CreatedDate = DateTime.Now;
        }

        public clsDriver(int driverID, 
                         int personID, 
                         int createdByUserID,
                         DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }

        public static DataTable GetAllDrivers()
        {
            return DriverData.GetAllDrivers();
        }

        public static clsDriver GetDriver(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (DriverData.GetDriver(DriverID, 
                                     ref PersonID,
                                     ref CreatedByUserID,
                                     ref CreatedDate)
                                     )
                return new clsDriver(DriverID,
                                     PersonID,
                                     CreatedByUserID,
                                     CreatedDate);
            return null; 
        }
        public bool Save()
        {
            DriverID= DriverData.AddNewDriver(PersonID,
                                              CreatedByUserID,
                                              CreatedDate);
            return DriverID != -1;            
        }
        public static DataTable Filter(string Filter , object Field)
        {
            return DriverData.Filter(Filter, Field);
        }

        public int GetLicense_Class3_OfDriver()
        {
            return (int)DriverData.GetLicense_Class3_Driver(DriverID);
        }

        public bool Has_Active_International_License()
        {
            return DriverData.Has_Active_International_License(DriverID) != null;
        }

        public DataTable GetAllLicensesOfDriver()
        {
            return LicenseData.GetAllLicensesOfDriver(DriverID);
        }

        public static int isDriver(int PersonID)
        {
            return DriverData.IsDriver(PersonID);
        }

    }
}
