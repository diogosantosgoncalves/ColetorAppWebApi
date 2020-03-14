using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorAPP.UWP
{
    class FileAccessHelper
    {
        public static string GetLocalFilePath(String filename)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);

        }
    }
}
