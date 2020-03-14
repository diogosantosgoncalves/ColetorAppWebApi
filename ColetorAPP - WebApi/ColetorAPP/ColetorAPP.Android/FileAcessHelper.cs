using System;

namespace ColetorAPP.Droid
{
    class FileAcessHelper
    {
        public static string GetLocalFilePath(String filename)
        {
            string path = System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal);
            //return System.IO.Path.Combine(path, filename);
            return System.IO.Path.Combine(path, filename);

        }
    }

}