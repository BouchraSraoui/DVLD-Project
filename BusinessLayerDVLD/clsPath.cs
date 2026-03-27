using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerDVLD
{
    public class clsPath
    {
        private static string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        public static string filepath = Path.Combine(projectRoot, "Images");
    }
}
