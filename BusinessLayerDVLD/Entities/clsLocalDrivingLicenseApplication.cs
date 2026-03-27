using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerDVLD.enums;
using DataAccessLayerDVLD;

namespace BusinessLayerDVLD
{
    public class clsLocalDrivingLicenseApplication
    {

        clsenAddUpdateMode _Mode;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }

        private clsApplication _Application;
        public clsApplication Application 
        {  get
            {
                if(_Application == null && ApplicationID > 0)
                    _Application = clsApplication.GetApplicationInfoByID(ApplicationID);
                return _Application;
            }
            set 
            {
                _Application = value;
                if (value != null)
                    ApplicationID = value.ApplicationID;
            } 
        }    
        public int LicenseClassID {  get; set; }
        private clsLicenseType _LicenseClass { get; set; }
        public clsLicenseType LicenseClass
        {
            get
            {
                if (_LicenseClass == null)
                    _LicenseClass = clsLicenseType.GetLicenseClass(LicenseClassID);

                return _LicenseClass;

            }
            set 
            {
                _LicenseClass = value;
                if(value != null)
                   LicenseClassID = value.LicenseClassId;
            }
        }
        public int PassedTests {  get; set; }

        public clsLocalDrivingLicenseApplication (clsApplication application, clsLicenseType licenseClass)
        {
            Application = application;
            LicenseClass = licenseClass;

            _Mode = clsenAddUpdateMode.ADD;
        }
        public clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            ApplicationID = applicationID;
            LicenseClassID = licenseClassID;

            _Mode = clsenAddUpdateMode.UPDATE;
        }
       public clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID, int PassedTest)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            ApplicationID = applicationID;
            LicenseClassID = licenseClassID;
            PassedTests = PassedTest;

            _Mode = clsenAddUpdateMode.UPDATE;
        }

        public static clsLocalDrivingLicenseApplication FindLDApp(int LDLAppID)
        {
            int ApplicationId = -1;
            int LicenseClassID = -1;

            if (LocalDrivingLicenseApplicationData.FindLDApp(LDLAppID,
                                                            ref ApplicationId,
                                                            ref LicenseClassID))
                return new clsLocalDrivingLicenseApplication(LDLAppID,
                                                             ApplicationId,
                                                             LicenseClassID);
            return null;
        } 
        
       public static clsLocalDrivingLicenseApplication GetLDLAppWithTestResult(int LDLAppID)
        {
            int ApplicationId = -1;
            int LicenseClassID = -1;
            int PassedTests = 0;

            if (LocalDrivingLicenseApplicationData.FindLDAppWithTestResult(LDLAppID,
                                                            ref ApplicationId,
                                                            ref LicenseClassID,
                                                            ref PassedTests))
                return new clsLocalDrivingLicenseApplication(LDLAppID,
                                                             ApplicationId,
                                                             LicenseClassID,
                                                             PassedTests);
            return null;
        }

        public static DataTable GetAllLDLApplication()
        {
            return LocalDrivingLicenseApplicationData.GetLDLAppsTestResults();
        }

        public static int GetLicenseClassID(int AppID)
        {
            return LocalDrivingLicenseApplicationData.GetLicenseClassID(AppID);
        }

        private bool AddNewLDLApplication()
        {
            LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationData.AddNewLDLApplication(Application.ApplicationID,
                                                                                                       LicenseClass.LicenseClassId);

            return LocalDrivingLicenseApplicationID != -1;
        }

        private bool UpdateLDLApplication()
        {
            return  LocalDrivingLicenseApplicationData.UpdateLDLApplication(LocalDrivingLicenseApplicationID,
                                                                            LicenseClass.LicenseClassId);
        }

        public bool Save()
        {   
            if(_Mode == clsenAddUpdateMode.ADD)
            {
                _Mode = clsenAddUpdateMode.UPDATE;
                return AddNewLDLApplication();
            }

            return UpdateLDLApplication();
        }

        public static DataTable Filter(string Filter, object Field)
        {
            return LocalDrivingLicenseApplicationData.Filter(Filter, Field);
        }

    }
}
