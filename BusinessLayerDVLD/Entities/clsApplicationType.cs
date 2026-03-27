using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerDVLD;
namespace BusinessLayerDVLD
{
    public class clsApplicationType
    {
        public int ApplicationTypeID {  get; set; }
        public string ApplicationTypeTitle {  get; set; }
        public double ApplicationTypeFees {  get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = string.Empty;//to aviod NULLReferenceException
            ApplicationTypeFees = 0;
        }
        public clsApplicationType(int applicationTypeID, 
                                  string applicationTypeTitle, 
                                  double applicationTypeFees)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationTypeFees = applicationTypeFees;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationTypeData.GetAllApplicationTypes();
        }

        public static clsApplicationType GetApplicationInfoByIDType(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            double ApplicationFees = 0;
            if (ApplicationTypeData.GetApplicationInfoByIDType(ApplicationTypeID,
                                                       ref ApplicationTypeTitle,
                                                       ref ApplicationFees)
                                                       )
                return new clsApplicationType(ApplicationTypeID,
                                              ApplicationTypeTitle,
                                              ApplicationFees);
            return null;
        }

        public bool Save()
        {
            return ApplicationTypeData.UpdateAppTypeInfo(ApplicationTypeID,
                                                         ApplicationTypeTitle,
                                                         ApplicationTypeFees);
        }
    }
}
