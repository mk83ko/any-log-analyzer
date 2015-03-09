using System.IO;

namespace Mkko
{
    public class FilesystemIoHelper
    {
        public static bool FileExists(string uri)
        {
            return File.Exists(uri);
        }

        public static StreamReader GetStreamReader(string uri)
        {
            try
            {
                return new StreamReader(GetFileStreamForRead(uri));
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

        private static FileStream GetFileStreamForRead(string uri)
        {
            FileStream file;
            try
            {
                file = new FileStream(uri, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (IOException io)
            {
                try
                {
                    file = new FileStream(uri, FileMode.Open, FileAccess.Read);
                }
                catch (IOException inner_io)
                {
                    file = GetCopyOfLockedFile(uri);
                }
            }
            return file;
        }

        private static FileStream GetCopyOfLockedFile(string uri)
        {
            try
            {
                var tempDir = System.Environment.GetEnvironmentVariable("TEMP");
                var tempFullQualifiedFilename = uri.Replace(@"\", "/").Split('/');
                var destinationFilename = tempDir + "/" +
                                          tempFullQualifiedFilename[tempFullQualifiedFilename.Length - 1];
                File.Copy(uri, destinationFilename);

                return new FileStream(destinationFilename, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
