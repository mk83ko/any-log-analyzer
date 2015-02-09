using System;
using System.IO;

namespace Mkko
{
    class FilesystemIoHelper
    {
        public static bool FileExists(string uri)
        {
            return File.Exists(uri);
        }

        public static FileInfo GetFileInfo(string uri)
        {
            return new FileInfo(uri);
        }

        public static StreamReader GetStreamReader(string uri)
        {
            try
            {
                return new StreamReader(uri);
            }
            catch (IOException ioe)
            {
                return null;
            }
        }

        public static StreamWriter GetStreamWriter(string uri)
        {
            return new StreamWriter(uri);
        }

        private static void copyLockedFile(string uri)
        {
            
        }
    }
}
