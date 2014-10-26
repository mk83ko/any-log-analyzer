using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mkko.EventDefinition;
using Mkko.LogFileReader;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Mkko.AnyLogAnalyzerTests.Core
{
    [TestFixture]
    public class SimpleLogReaderTest
    {
        private static string ExecutingAssemblyPath =
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
        private const string EmbeddedJbossLogfile = "Mkko.AnyLogAnalyzerTests.res.jboss-boot.log";
        private const string EmbeddedJsonDefinitionsFileForJbossLog = "Mkko.AnyLogAnalyzerTests.res.jboss.json";

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NotExistingFileThrowsFileNotFoundException()
        {
            var logfile = SimpleLogReaderTest.ExecutingAssemblyPath + "this-file-wont.exist";
            var reader = this.GetSimpleLogReader(logfile, SimpleLogReaderTest.EmbeddedJsonDefinitionsFileForJbossLog);
            this.Iterate(reader);
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void LogfileUriNotSpecified()
        {
            var jsonDefinition = SimpleLogReaderTest.EmbeddedJsonDefinitionsFileForJbossLog;
            var reader = this.GetSimpleLogReader(null, jsonDefinition);
            this.Iterate(reader);
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void DefinitionsNotSpecified()
        {
            var jbossLog = SimpleLogReaderTest.EmbeddedJbossLogfile;
            var reader = this.GetSimpleLogReader(jbossLog, null);
            this.Iterate(reader);
        }

        [Test]
        public void FindsExpectedEventInLogfile()
        {
            var jbossLog = SimpleLogReaderTest.EmbeddedJbossLogfile;
            var jsonDefinition = SimpleLogReaderTest.EmbeddedJsonDefinitionsFileForJbossLog;
            var reader = this.GetSimpleLogReader(jbossLog, jsonDefinition);
            var numberOfEvents = 0;
            foreach (var logEvent in reader.GetEventIterator())
            {
                numberOfEvents++;
            }
            Assert.AreEqual(1, numberOfEvents);
        }

        private void Iterate(ILogFileReader reader)
        {
            foreach (var logEvent in reader.GetEventIterator())
            {
                Console.WriteLine(logEvent.Category);
            }
        }

        private SimpleLogReader GetSimpleLogReader(string logfile, string definitions)
        {
            StreamReader streamReader = null;
            StreamReader jsonDefinitons = null;
            if (logfile != null)
            {
                streamReader = this.GetEmbeddedResourceAsStreamReader(logfile);
            }
            if (definitions != null)
            {
                jsonDefinitons = this.GetEmbeddedResourceAsStreamReader(definitions);
            }

            var reader = new SimpleLogReader(logfile, streamReader) {EventDefinition = new JsonEventParser(jsonDefinitons)};

            return reader;
        }

        private StreamReader GetEmbeddedResourceAsStreamReader(string fileOrResource)
        {
            StreamReader streamReader = null;
            try
            {
                // check if resource is embedded into assembly (for tests with actual files)
                var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileOrResource);
                streamReader = new StreamReader(resourceStream);
            }
            catch (ArgumentNullException)
            {
                // else assume that given string is a file URI
                streamReader = new StreamReader(fileOrResource);
            }
            return streamReader;
        }
    }
}
