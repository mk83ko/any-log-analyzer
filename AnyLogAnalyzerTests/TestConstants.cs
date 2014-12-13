using System;
using System.IO;
using System.Reflection;

namespace Mkko.AnyLogAnalyzerTests
{
    public class TestConstants
    {
        internal const string EmbeddedJbossLogfile = "Mkko.AnyLogAnalyzerTests.res.jboss-boot.log";
        internal const string EmbeddedJsonDefinitionsFileForJbossLog = "Mkko.AnyLogAnalyzerTests.res.jboss.json";

        internal static string ExecutingAssemblyPath =
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
    }
}