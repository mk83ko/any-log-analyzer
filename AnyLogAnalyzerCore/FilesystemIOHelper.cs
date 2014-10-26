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
            return new StreamReader(uri);
        }

        public static StreamWriter GetStreamWriter(string uri)
        {
            return new StreamWriter(uri);
        }
    }
}
