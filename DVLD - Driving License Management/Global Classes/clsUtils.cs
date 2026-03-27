using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD___Driving_License_Management.Global_Classes
{
    internal class clsUtils
    {
        private static Guid _GenerateGuid()
        {
            return Guid.NewGuid();
        }
        private static string _NewPath(string FileName)
        {
            if (FileName == "")
                return "";

            string ext = Path.GetExtension(FileName);

            string NewPath = _GenerateGuid() + ext;
            return NewPath;
        }

        public static string CopyImageToFolder( string SourceFile , string filepath)
        {
           string NewPath = _NewPath(SourceFile);

           if (string.IsNullOrEmpty(NewPath))
               return "";

            string FullPath = filepath + "\\" + NewPath;
            try
            {
                File.Copy(SourceFile, FullPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return NewPath;
            
        }
    }
}
