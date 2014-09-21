using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    class FilesystemIOHelper
    {
        public static bool fileExists(string uri)
        {
            return File.Exists(uri);
        }

        public static FileInfo getFileInfo(string uri)
        {
            return new FileInfo(uri);
        }

        public static StreamWriter openFileForWriting(string uri)
        {
            return new StreamWriter(uri);
        }
    }
}
