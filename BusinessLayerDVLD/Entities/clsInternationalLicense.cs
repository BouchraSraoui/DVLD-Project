using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;

namespace BusinessLayerDVLD
{
    public class clsInternationalLicense 
    {
        enum enMode
        {
            Add,
            Update
        }
        enMode _Mode;
        public int InternationalLicenseID { get; set; }
        public clsApplication Application { get; set; }
        public clsLicense LocalLicense { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool ISActive { get; set; }
        public clsUser CreatedByUser { get; set; }
        public clsApplicationType ApplicationType { get; set; }
        public clsInternationalLicense(clsLicense LocalLicense,
                                       clsUser CreatedByUser)
        {
            
            this.LocalLicense = LocalLicense;
            this.CreatedByUser = CreatedByUser;
            IssueDate = DateTime.Now;
            ExpirationDate = IssueDate.AddYears(1);
            ISActive = true;

            
            _Mode = enMode.Add;
        }
        public clsInternationalLicense(int internationalLicenseID,
                                       int ApplicationID,
                                       int localLicenseId,
                                       DateTime issueDate,
                                       DateTime expirationDate,
                                       bool iSActive,
                                       int createdByUserID)
        {

            InternationalLicenseID = internationalLicenseID;
            Application = clsApplication.GetApplicationInfoByID(ApplicationID);
            LocalLicense = clsLicense.GetLicense(localLicenseId);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            ISActive = iSActive;
            CreatedByUser = clsUser.GetUserByID(createdByUserID);

            _Mode= enMode.Update;
        }

        public static clsInternationalLicense GetInterLicense(int InterantionalLicenseID)
        {
            int LocalLicenseID = -1;
            int ApplicationId = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;

            if (InternationLicenseData.GetInterLicense(InterantionalLicenseID,
                                                       ref LocalLicenseID,
                                                       ref ApplicationId,
                                                       ref IssueDate,
                                                       ref ExpirationDate,
                                                       ref IsActive,
                                                       ref CreatedByUserID)
                                                      )
                return new clsInternationalLicense(InterantionalLicenseID,
                                                   ApplicationId,
                                                   LocalLicenseID,
                                                   IssueDate,
                                                   ExpirationDate,
                                                   IsActive,
                                                   CreatedByUserID);
            return null;
        }

        public static DataTable GetInterLicenseOFLocalLicense(int LocalLicense)
        {
            return InternationLicenseData.GetInterLicenseOFLocalLicense(LocalLicense);
        }

        private bool _AddNewLicense()
        {
            InternationalLicenseID = InternationLicenseData.AddNewLicense(Application.ApplicationID,
                                                                          LocalLicense.Driver.DriverID,
                                                                          LocalLicense.LicenseID,
                                                                          IssueDate,
                                                                          ExpirationDate,
                                                                          ISActive,
                                                                          CreatedByUser.UserID);
                            
            return InternationalLicenseID != -1;
        }

        private bool _UpdateLicenseInfo()
        {
            return InternationLicenseData.UpdateLicenseInfo(InternationalLicenseID,
                                                            Application.ApplicationID,
                                                            LocalLicense.LicenseID,
                                                            IssueDate,
                                                            ExpirationDate,
                                                            ISActive,
                                                            CreatedByUser.UserID);

        }

        private void _Create_Save_NewApplication()
        {
            clsApplication NewApplication = new clsApplication(ApplicationType,
                                                                this.LocalLicense.Driver.Person,
                                                                this.CreatedByUser);
            NewApplication.Save();

            Application = NewApplication;
        }

        public bool Save()
        {
            if (_Mode == enMode.Add)
            {
                _Create_Save_NewApplication();
                return _AddNewLicense();
            }
                

            return _UpdateLicenseInfo();

        }
        public bool Disactive()
        {
            return InternationLicenseData.DisactiveLicense(InternationalLicenseID);
        }

        public bool IsExpired()
        {
            if (DateTime.Now > ExpirationDate)
            {
                Disactive();
                return true;
            }

            return false;

        }

        public static DataTable GetAllIntLicenses()
        {
            return InternationLicenseData.GetAllIntLicenses();
        }

        public static DataTable Filter(string Filter, object Field)
        {
            return InternationLicenseData.Filter(Filter, Field);
        }
    }
}
