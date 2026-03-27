using BusinessLayerDVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DVLD___Driving_License_Management.Global_Classes
{
    internal class clsGlobal
    {
        public static bool RememberUsernameAndPassword(string username , string password)
        {
            try
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

                string filepath = Path.Combine(projectRoot, "UserRemember.txt");

                if(username.Trim() == "" && File.Exists(filepath))
                {
                    File.Delete(filepath);
                    return true;
                }

                string UserInfo = username + "#//#" + password;

                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    writer.WriteLine(UserInfo);

                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error : {ex.Message}");
                return false;
            }
        }

        public static bool ReadInfoFromFile(ref string username , ref string password)
        {
            try
            {
                string CurrentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string filepath = Path.Combine(CurrentDirectory, "UserRemember.txt");

                using (StreamReader reader = new StreamReader(filepath))
                {
                    string line = reader.ReadLine();

                    string[] userinfo = line.Split(new string[] { "#//#" }, StringSplitOptions.RemoveEmptyEntries);

                    if(userinfo.Length ==2) 
                    {
                         username = userinfo[0];
                         password = userinfo[1];
                    }

                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error :{ex.Message}");
                return false;
            }
        }

    }
}

