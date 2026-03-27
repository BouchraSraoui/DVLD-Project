using DataAccessLayerDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerDVLD
{
    public class clsTestType
    {
        public int TestTypeID {  get; set; }
        public string TestTypeTitle {  get; set; }
        public string TestTypeDescription { get; set; }
        public double TestTypeFees {  get; set; }
        private enum eenTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType(int testTypeID, string testTypeTitle, string testTypeDescription, double testTypeFees)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static DataTable GetAllTestTypes()
        {
            return TestTypeData.GetAllTestTypes();
        }

        public static clsTestType GetTestType(int TestTypeID)
        {
            string testTypeTitle = "";
            string testTypeDescription = "";
            double testTypeFees = 0;
            if ( TestTypeData.GetTestType(TestTypeID,
                                          ref testTypeTitle,
                                          ref testTypeDescription,
                                          ref testTypeFees))
                return new clsTestType(TestTypeID,
                                       testTypeTitle,
                                       testTypeDescription,
                                       testTypeFees);

            return null;
        }                             
        public bool Save()
        {
            return TestTypeData.UpdateTestTypeInfo(TestTypeID,
                                                   TestTypeTitle,
                                                   TestTypeDescription,
                                                   TestTypeFees);
        }
            
    }
}
