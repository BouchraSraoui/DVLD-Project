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
    public class clsLicenseType
    {
        public int LicenseClassId {  get; set; }
        public string ClassName {  get; set; }
        public string Description { get; set; }
        public int MinimumAllowedAge {  get; set; }
        public int DefaultValidityLength {  get; set; }
        public double ClassFees {  get; set; }

        public clsLicenseType(int licenseClassId,
                              string className,
                              string description,
                              int minimumAllowedAge,
                              int defaultValidityLength,
                              double classFees)
        {
            LicenseClassId = licenseClassId;
            ClassName = className;
            Description = description;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
        }



        public static clsLicenseType GetLicenseClass(int LicenseClassId)
        {
            string ClassName = "";
            string Description = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            double ClassFees = 0;

            if(LicenseClassData.GetLicenseTypeByID(LicenseClassId,
                                                   ref ClassName,
                                                   ref Description,
                                                   ref MinimumAllowedAge,
                                                   ref DefaultValidityLength,
                                                   ref ClassFees)) 
                return new clsLicenseType(LicenseClassId,
                                          ClassName,
                                          Description,
                                          MinimumAllowedAge,
                                          DefaultValidityLength,
                                          ClassFees);
            return null;
                                         

        }

        public static DataTable GetAllLicenseClasses()
        {
            return LicenseClassData.GetAllLicenseClasses();
        }

    }
}
