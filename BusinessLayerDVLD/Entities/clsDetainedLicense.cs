using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;

namespace BusinessLayerDVLD
{
    public class clsDetainedLicense
    {
        enum enDetainStatus 
        { 
            Detain = 1,
            Release=2
        }
        enDetainStatus _DetainStatus;
        public int DetainID {  get; set; }
        public int LicenseID {  get; set; }

        private clsLicense _License;
        public clsLicense License
        {
            get
            {
                if (_License == null && LicenseID >0)
                    _License = clsLicense.GetLicense(LicenseID);
                return _License;
            }
            set 
            {
                License = value;
                if(value != null)
                    LicenseID = value.LicenseID;

            }
        }
        public DateTime DetainDate { get; set; }
        public double FineFees {  get; set; }
        public int CreatedByUserID {  get; set; }

        private clsUser _CreatedByUser;
        public clsUser CreatedByUser 
        { 
            get
            {
                if (_CreatedByUser == null && CreatedByUserID >0)
                    _CreatedByUser = clsUser.GetUserByID(CreatedByUserID);
                return _CreatedByUser;
            } 
            set 
            {
                _CreatedByUser = value;
                if(value != null)
                    CreatedByUserID = value.UserID;
            } 
        }
        public bool IsReleased {  get; set; }
        public DateTime ReleaseDate { get; set; }
        public clsApplication ReleaseApplication { get; set; }   

        public clsDetainedLicense(clsLicense license , clsUser CreatedByUser)
        {
            this.License = license;
            LicenseID = License.LicenseID;
            this.CreatedByUser = CreatedByUser;
            CreatedByUserID = CreatedByUser.UserID;
            DetainDate = DateTime.Now;

            _DetainStatus = enDetainStatus.Detain;
        }
        public clsDetainedLicense(int DetainID, 
                                  int LicenseID, 
                                  DateTime DetainDate, 
                                  double FineFees, 
                                  int CreatedByUserID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;

            _DetainStatus = enDetainStatus.Release;
        }

        private bool _DetainedLicense()
        {
            DetainID = DetainLicenseData.AddNewDetainedLicense(LicenseID,
                                                               DetainDate,
                                                               FineFees,
                                                               CreatedByUserID);
            return DetainID != -1;
        }
        private bool _ReleaseLicense()
        {
            return DetainLicenseData.ReleaseLicense(DetainID,
                                                    IsReleased,
                                                    ReleaseDate,
                                                    ReleaseApplication.CreatedByUserID,
                                                    ReleaseApplication.ApplicationID);
        }

        public bool save()
        {
            if (_DetainStatus == enDetainStatus.Detain)
            {
                _DetainStatus = enDetainStatus.Detain;
                return _DetainedLicense();
            }
               
            return _ReleaseLicense();
        }

        public static clsDetainedLicense GetDetainLicenseInfo(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            double FineFees = 0;
            int createdByUserID = -1;

            if (DetainLicenseData.GetDetainLicenseInfo(LicenseID,
                                                      ref DetainID,
                                                      ref DetainDate,
                                                      ref FineFees,
                                                      ref createdByUserID)
                                                      )
                return new clsDetainedLicense(DetainID,
                                              LicenseID,
                                              DetainDate,
                                              FineFees,
                                              createdByUserID);
            return null;
                 
        }

        public static DataTable GetAllDetainedLicenses()
        {
           return DetainLicenseData.GetAllDetainedLicenses();
        }

        public static DataTable Filter(string Filter , object Field)
        {
            return DetainLicenseData.Filter(Filter , Field);
        }
    }
}
