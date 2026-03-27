using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerDVLD
{
    public enum enApplicationType
    {
        New_Local_Driving_License_Service = 1,
        Renew_Driving_License_Service = 2,
        Replacement_for_Lost_Driving_License = 3,
        Replacement_for_Damaged_Driving_License= 4,
        Release_Detained_Driving_Licsense=5,
        New_International_License = 6,
        Retake_Test=7
    }
}
