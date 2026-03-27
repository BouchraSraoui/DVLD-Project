using DataAccessLayerDVLD;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BusinessLayerDVLD
{
    public class clsUser
    {
        enum enEDPasswd
        {
            Encrypt,
            Decrypt
        }
        enum enMode
        {
            Add,
            Update
        }
        enMode _Mode = enMode.Add;
        public int UserID { get; set; }
        public clsPerson Person { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ISActive { get; set; }


        public clsUser(clsPerson Person)
        {
            UserID = -1;
            this.Person = Person;
            UserName = "";
            Password = "";
            ISActive = true;

            _Mode = enMode.Add;
        }

        public clsUser(int userID,
                       int personID,
                       string userName,
                       string password,
                       bool isActive)
        {
            UserID = userID;
            Person = clsPerson.FindPersonByID(personID);
            UserName = userName;
            Password = password;
            ISActive = isActive;

            Password = _Encrypt_Decrypt_PassWord(password, enEDPasswd.Decrypt);

            _Mode = enMode.Update;
        }

        private static string _Encrypt_Decrypt_PassWord(string password, enEDPasswd EDPasswd)
        {
            string result = "";

            for (int i = 0; i < password.Length; i++)
            {
                if (EDPasswd == enEDPasswd.Encrypt)
                    result += (char)((int)password[i] + 2);

                else
                    result += (char)((int)password[i] - 2);

            }

            return result;
        }

        public static clsUser GetUser(string UserName, string password)
        {

            int UserId = -1;
            bool ISActive = true;
            int PersonID = -1;

            password = _Encrypt_Decrypt_PassWord(password, enEDPasswd.Encrypt);

            if (UserData.GetUser(UserName, password,
                                 ref UserId, ref PersonID,
                                 ref ISActive))
                return new clsUser(UserId,
                                   PersonID,
                                   UserName,
                                   password,
                                   ISActive);
            return null;

        }

        public static clsUser GetUserByID(int UserID)
        {
            string UserName = "";
            string Password = "";
            bool ISActive = true;
            int PersonID = -1;


            if (UserData.GetUserByID(UserID,
                                 ref UserName,
                                 ref Password,
                                 ref PersonID,
                                 ref ISActive))
                return new clsUser(UserID,
                                   PersonID,
                                   UserName,
                                   Password,
                                   ISActive);
            return null;
        }

        public static int FindUserByUserName(string UserName)
        {
            object UserID = UserData.FindUserByUserName(UserName);

            return UserID == null ? -1 : (int)UserID;
        }

        private bool _AddNewUser()
        {
            UserID = UserData.AddNewUser(Person.PersonID,
                                       UserName,
                                       Password,
                                       ISActive);
            return UserID != -1;
        }

        private bool _UpdateUserInfo()
        {

            return UserData.UpdateUserInfo(UserID,
                                          Person.PersonID,
                                          UserName,
                                          Password,
                                          ISActive);
        }

        public bool Save()
        {
            Password = _Encrypt_Decrypt_PassWord(Password, enEDPasswd.Encrypt);

            if (_Mode == enMode.Add)
            {
                _Mode = enMode.Update;
                return _AddNewUser();
            }

            return _UpdateUserInfo();
        }

        public static DataTable GetAllUsers()
        {
            return UserData.GetAllUsers();
        }

        public static DataTable Filter(string Filter, object Field)
        {
            return UserData.Filter(Filter, Field);
        }

        public bool HasData()
        {
            return Convert.ToBoolean(UserData.UserHasData(UserID));
        }

        public bool Delete()
        {
            return UserData.Delete(UserID);
        }

        public bool IsExist()
        {
            return UserData.ISExist(UserID) != -1;
        }

        public static bool isUser(int PersonID)
        {
            return Convert.ToBoolean(UserData.IsUser(PersonID));
        }

    }

}

