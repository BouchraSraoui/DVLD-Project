using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayerDVLD;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Collections;
using System.IO;
namespace BusinessLayerDVLD
{
    
    public class clsPerson
    {
        enum enMode
        {
            Add,
            Update
        }

        enMode _Mode;
        public int PersonID {  get; set; }
        public string NationalNo {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string SecondName {  get; set; }
        public string Address { get; set; } 
        public string ThirdName {  get; set; }
        public int CountryId {  get; set; }
        public string Gendor {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Email {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath {  get; set; }
        public int Age
        {
            get
            {
                return age();
            }
        }
        public string FullName
        {
            get
            {
                return FirstName + ' ' + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public clsPerson()
        {
            PersonID = 0;
            NationalNo = "";
            FirstName = "";
            LastName = "";
            SecondName = "";
            ThirdName = "";
            Address = "";
            CountryId = 0;
            Gendor = "";
            PhoneNumber = "";
            Email = "";
            DateOfBirth = DateTime.Now.AddYears(-18);
            ImagePath = clsPath.filepath;

            _Mode = enMode.Add;
        }
        public clsPerson(int personID, string nationalNo, string firstName, string lastName,
                 string secondName, string thirdName, string address, int countryId,
                 string gendor, string phoneNumber, string email, DateTime dateOfBirth,
                 string imagePath)
        { 
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
            ThirdName = thirdName;
            Address = address;
            CountryId = countryId;
            Gendor = gendor;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            ImagePath = Path.Combine(clsPath.filepath, imagePath);

            _Mode = enMode.Update;
        }

        public static DataTable GetAllPeople()
        {
            return PersonData.GetAllPeople();
        }

        public static DataTable Filter(string Filter, object Field )
        {
            return PersonData.Filter( Filter, Field );
        }

        private void _DeleteImageRelatedToPerson()
        {
            if (ImagePath == clsPath.filepath)
                return;
            try
            {
                    if(File.Exists(ImagePath))
                    {
                        File.Delete(ImagePath);
                    }
            }
            catch(Exception ex)
            {

            }
            
        }

        public  bool DeletePerson()
        {
            _DeleteImageRelatedToPerson();

            return PersonData.DeletePerson(PersonID);
        }

        public static clsPerson FindPersonByID(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now.AddYears(-18);
            string Gendor = "";
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";

            if (PersonData.FindPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName,
                                          ref ThirdName, ref LastName,ref DateOfBirth,  ref Gendor,
                                          ref Address,  ref Phone,  ref Email, ref NationalityCountryID, ref ImagePath))
                return new clsPerson(PersonID,
                                     NationalNo,
                                     FirstName,
                                     LastName,
                                     SecondName,
                                     ThirdName,
                                     Address,
                                     NationalityCountryID,
                                     Gendor,
                                     Phone,
                                     Email,
                                     DateOfBirth,
                                     ImagePath);
           return null;
                    
        }

        public static int IsPersonExist(string NationalNo)
        {
            return PersonData.IsPersonExist(NationalNo.Trim());
        }
        private bool _AddNewPerson()
        {

            PersonID = PersonData.AddNewPerson(NationalNo,
                                               FirstName,
                                               SecondName,
                                               ThirdName,
                                               LastName,
                                               DateOfBirth,
                                               Gendor,
                                               Address,
                                               PhoneNumber,
                                               Email,
                                               CountryId,
                                               ImagePath);

            return PersonID != -1;


        }                                      

        private bool _UpdatePersonInfo()       
        {


             return PersonData.UpdatePersonInfo(PersonID,
                                                NationalNo,
                                                FirstName,
                                                SecondName,
                                                ThirdName,
                                                LastName,
                                                DateOfBirth,
                                                Gendor,
                                                Address,
                                                PhoneNumber,
                                                Email,
                                                CountryId,
                                                ImagePath);

        }                                      
                                               
        public bool Save()                     
        {
           this.ImagePath = ImagePath.Replace(clsPath.filepath+"\\", "");

            if (_Mode == enMode.Add)
            {
                _Mode = enMode.Update;
                return _AddNewPerson();
            }
                                                           
            return _UpdatePersonInfo();
        }

        private int age()
        {
            DateTime today = DateTime.Now;
           int age = today.Year - DateOfBirth.Year;

            if ((DateOfBirth.Month == today.Month && today.Day < DateOfBirth.Day) 
                ||
                (DateOfBirth.Month > today.Month))
                --age;

            return age;
        }

        public bool HasRelatedWithData()
        {
            return clsDriver.isDriver(PersonID) != -1
                   ||
                   clsUser.isUser(PersonID)
                   ||
                   clsApplication.HasApplication(PersonID);
        }

    }
}
