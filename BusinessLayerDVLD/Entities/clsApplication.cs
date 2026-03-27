using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerDVLD.enums;
using DataAccessLayerDVLD;
namespace BusinessLayerDVLD
{
    public class clsApplication
    {
        clsenAddUpdateMode _Mode;
        public int ApplicationID {  get; set; }
        public int PersonID {  get; set; }
        private clsPerson _Person;
        public clsPerson Person 
        { get 
            { 
                if(_Person == null)
                    _Person = clsPerson.FindPersonByID(PersonID);
                return _Person;
            }
            set
            {
               _Person = value;
                if(value != null)
                    PersonID = value.PersonID;
            }
        }
        public int ApplicationTypeID {  get; set; }

        private clsApplicationType _ApplicationType;
        public clsApplicationType ApplicationType
        {
            set
            {
                _ApplicationType = value;
                if(value != null)
                    ApplicationTypeID = value.ApplicationTypeID;
            }
            get 
            {
                if(_ApplicationType == null)
                    _ApplicationType = clsApplicationType.GetApplicationInfoByIDType(ApplicationTypeID); ;
                return _ApplicationType;
            }
        }
        public double PaidFees {  get; set; }
        public int CreatedByUserID {  get; set; }

        private clsUser _CreatedByUser;
        public clsUser CreatedByUser 
        {
            set
            {
                _CreatedByUser = value;
                if(value !=null)
                    CreatedByUserID = value.UserID;
            }
            get
            {
                if (_CreatedByUser == null && CreatedByUserID >0)
                    _CreatedByUser = clsUser.GetUserByID(CreatedByUserID);
                return _CreatedByUser;    
            }
           
        }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public enApplicationStatus ApplicationStatus {  get; set; }

        
        public clsApplication(clsApplicationType applicationType, 
                              clsPerson person, 
                              clsUser createdByUser)
        {
            this.ApplicationType = applicationType;
            this.Person = person;
            this.CreatedByUser = createdByUser;
            PaidFees = applicationType.ApplicationTypeFees;
            ApplicationDate = DateTime.Now;
            LastStatusDate = DateTime.Now;
            ApplicationStatus = enApplicationStatus.New;
            _Mode = clsenAddUpdateMode.ADD;

        }

        public clsApplication(int applicationID, int personID, int applicationTypeID, 
            double paidFees, int createdByUserID, DateTime applicationDate, 
            DateTime lastStatusDate, int applicationStatus)
        {
            _Mode = clsenAddUpdateMode.UPDATE;
            ApplicationID = applicationID;
            PersonID = personID;
            ApplicationTypeID = applicationTypeID;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            ApplicationDate = applicationDate;
            LastStatusDate = lastStatusDate;
            ApplicationStatus = (enApplicationStatus)applicationStatus;
        }


        public static clsApplication GetApplicationInfoByID(int ApplicationID)
        {
            int PersonID = -1;
            int ApplicationTypeID = -1;
            int CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now;
            DateTime LastStatusDate = DateTime.Now;
            int ApplicationStatus = 1;
            double PaidFees = 0;

            if (ApplicationData.GetApplicationInfoByID(
                                              ApplicationID,
                                              ref PersonID,
                                              ref ApplicationTypeID,
                                              ref CreatedByUserID,
                                              ref ApplicationDate,
                                              ref LastStatusDate,
                                              ref ApplicationStatus,
                                              ref PaidFees))
                return new clsApplication(ApplicationID,
                    PersonID,
                    ApplicationTypeID,
                    PaidFees,
                    CreatedByUserID,
                    ApplicationDate,
                    LastStatusDate,
                    ApplicationStatus);
            return null;
        }

        public string StatusTOString()
        {
            switch (ApplicationStatus)
            {
                case enApplicationStatus.New:
                    return "New";
                case enApplicationStatus.Cancelled:
                    return "Cancelled";
                case enApplicationStatus.Completed:
                    return "Completed";
                default:
                    return "";
            }
        }

        public static enApplicationStatus applicationStatusINT(string Status)
        {
            if (Status.ToLower() == "new")
                return enApplicationStatus.New;

            if (Status.ToLower() == "cancelled")
                return enApplicationStatus.Cancelled;

            return enApplicationStatus.Completed;
        }

        public  bool DeleteApplication()
        {
            return ApplicationData.DeleteApplication(ApplicationID);
        }

        public static DataTable GetAppsRelatedPerson(int PersonID)
        {
            return ApplicationData.GetAppsRelatedPerson(PersonID);
        }

        private bool _AddNewApplication()
        {
            ApplicationID = ApplicationData.AddNewApplication(PersonID,
                                                            ApplicationTypeID,
                                                            PaidFees,
                                                            CreatedByUserID,
                                                            ApplicationDate,
                                                            LastStatusDate,
                                                            (int)ApplicationStatus);

            return ApplicationID != -1;
        }

        private bool _UpdateApplicationInfo()
        {
            LastStatusDate = DateTime.Now;

            return ApplicationData.UpdateApplicationInfo(ApplicationID,
                                                            PersonID,
                                                            ApplicationTypeID,
                                                            PaidFees,
                                                            CreatedByUserID,
                                                            ApplicationDate,
                                                            LastStatusDate,
                                                            (int)ApplicationStatus);

            
        }

        public bool Save()
        {
            if (_Mode == clsenAddUpdateMode.ADD)
            {
                _Mode = clsenAddUpdateMode.UPDATE;
                return _AddNewApplication();
            }
                
            return _UpdateApplicationInfo();
        }


        public static bool HasApplication(int PersonID)
        {
            return Convert.ToBoolean(ApplicationData.PersonHasApplication(PersonID));
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return ApplicationData.GetActiveApplicationID(PersonID, ApplicationTypeID) != -1;
        }

        public static bool DoesPersonHaveSameActiveLicenseType(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationData.GetApplicationIDForLicenseClass(PersonID, ApplicationTypeID, LicenseClassID) != -1;
        }

        public bool Cancel()
        {
            return UpdateStatus(enApplicationStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return UpdateStatus(enApplicationStatus.Completed);
        }
        private bool UpdateStatus(enApplicationStatus ApplicationSatatus)
        {
            ApplicationStatus = ApplicationSatatus;
            LastStatusDate = DateTime.Now;

            return ApplicationData.UpdateStatus(ApplicationID,
                                                (int)ApplicationStatus,
                                                LastStatusDate);
        }
    }
}
