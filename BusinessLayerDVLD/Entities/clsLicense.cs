using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;
namespace BusinessLayerDVLD
{
    public class clsLicense
    {
        enum enMode
        {
            Add,
            Update
        }
        enMode _Mode;
        public int LicenseID {  get; set; }
        public clsApplication Application { get; set; }
        public clsDriver Driver { get; set; }
        public clsLicenseType LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes {  get; set; }
        public double PaidFees {  get; set; }
        public bool ISActive {  get; set; }
        public int IssueReason {  get; set; }
        public clsUser CreatedByUser { get; set; }
        public bool isDeatined {  get; set; }
        public clsLicense (clsApplication Application, clsDriver driver,clsLicenseType licenseType,clsUser createdByUser)
        {
            this.Driver = driver;
            this.LicenseClass = licenseType;
            this.Application = Application;
            this.CreatedByUser = createdByUser;
            IssueDate = DateTime.Now;
            ExpirationDate = IssueDate.AddYears(LicenseClass.DefaultValidityLength);
            PaidFees = LicenseClass.ClassFees;
            ISActive = true;
            IssueReason = Application.ApplicationTypeID;
            _Mode = enMode.Add;


        }
        public clsLicense(int licenseID, 
                          int ApplicationID, 
                          int driverID, 
                          int licenseClassID, 
                          DateTime issueDate,
                          DateTime expirationDate, 
                          string notes, 
                          double paidFees,
                          bool iSActive, 
                          int issueReason, 
                          int createdByUserID)
        {
            LicenseID = licenseID;
            Application = clsApplication.GetApplicationInfoByID(ApplicationID);
            Driver = clsDriver.GetDriver(driverID);
            LicenseClass = clsLicenseType.GetLicenseClass(licenseClassID);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            ISActive = iSActive;
            IssueReason = issueReason;
            CreatedByUser = clsUser.GetUserByID(createdByUserID);

            isDeatined = IsDetained();
            _Mode = enMode.Update;
        }

        public static int GetLicenseIDByAPPId(int appID)
        {
            return LicenseData.GetLicenseIDByApplicationID(appID);
        }

        public bool IsExpired()
        {
            if( DateTime.Now > ExpirationDate)
            {
                Disactive();
                
                return true;
            }

            return false;

        }

        private bool AddNewLicense()
        {
            LicenseID = LicenseData.AddNewLicense(Application.ApplicationID,
                                                      Driver.DriverID,
                                                      LicenseClass.LicenseClassId,
                                                      IssueDate,
                                                      ExpirationDate,
                                                      Notes,
                                                      PaidFees,
                                                      ISActive,
                                                      IssueReason,
                                                      CreatedByUser.UserID);

            return LicenseID != -1;
        }

        private bool UpdateLicenseInfo()
        {
            return LicenseData.UpdateLicenseInfo(LicenseID,
                                                     Application.ApplicationID,
                                                      Driver.DriverID,
                                                      LicenseClass.LicenseClassId,
                                                      IssueDate,
                                                      ExpirationDate,
                                                      Notes,
                                                      PaidFees,
                                                      ISActive,
                                                      IssueReason,
                                                      CreatedByUser.UserID);

          
        }

        public bool Save()
        {
            if (_Mode == enMode.Add)
                return AddNewLicense();

            return UpdateLicenseInfo();

        }

        

        public static clsLicense GetLicense(int LicenseID)
        {
             int driverID = -1;
             int licenseClassID = -1;
             DateTime issueDate = DateTime.Now;
             DateTime expirationDate = DateTime.Now;
             string notes = "";
             double paidFees = 0;
             bool iSActive = true;
             int issueReason = -1;
             int createdByUserID = -1;
            int ApplicationID = -1;

            if (LicenseData.GetLicense(LicenseID,
                                         ref ApplicationID,
                                         ref driverID,
                                         ref licenseClassID,
                                         ref issueDate,
                                         ref expirationDate,
                                         ref notes,
                                         ref paidFees,
                                         ref iSActive,
                                         ref issueReason,
                                         ref createdByUserID))
                return new clsLicense(LicenseID,
                                      ApplicationID,
                                      driverID,
                                      licenseClassID,
                                      issueDate,
                                      expirationDate,
                                      notes,
                                      paidFees,
                                      iSActive,
                                      issueReason,
                                      createdByUserID
                                      );
            return null;

        }

        public bool Disactive()
        {
            ISActive = false;
            return LicenseData.DisactiveLicense(LicenseID, ISActive);
        }
        public bool HasAlreadyActiveInterLicense()
        {
            object InternationalLicenseID = LicenseData.HasInternationalLicense(LicenseID);
            if (InternationalLicenseID == null)
                return false;

            clsInternationalLicense InternationalLicense = clsInternationalLicense.GetInterLicense(
                                                      Convert.ToInt32(InternationalLicenseID));

            if(InternationalLicense == null)
                return false;

            if (InternationalLicense.IsExpired())
                return false;

            return true;
        }

        public bool IsDetained()
        {
            object IsDetained = LicenseData.IsDetained(LicenseID);
            return IsDetained == null ? false :Convert.ToBoolean(IsDetained);
        }

        public static int GetActiveLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            return LicenseData.GetActiveLicenseByPersonID(PersonID, LicenseClassID);
        }
        public static bool IsLicenseExistsByPersonID(int PersonID, int LicenseClassID)
        {
            return LicenseData.GetActiveLicenseByPersonID(PersonID, LicenseClassID) != -1;
        }
    }
}
