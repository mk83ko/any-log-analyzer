using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mkko.EventDefinition;
using Mkko.LogFileReader;
using Mkko.AnyLogAnalyzerTests;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Mkko.AnyLogAnalyzerTests.Core
{
    [TestFixture]
    public class SimpleLogReaderTest
    {
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NotExistingFileThrowsFileNotFoundException()
        {
            var logfile = TestConstants.ExecutingAssemblyPath + "this-file-wont.exist";
            var reader = this.GetSimpleLogReader(logfile, TestConstants.EmbeddedJsonDefinitionsFileForJbossLog);
            this.Iterate(reader);
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void LogfileUriNotSpecified()
        {
            var jsonDefinition = TestConstants.EmbeddedJsonDefinitionsFileForJbossLog;
            var reader = this.GetSimpleLogReader(null, jsonDefinition);
            this.Iterate(reader);
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void DefinitionsNotSpecified()
        {
            var jbossLog = TestConstants.EmbeddedJbossLogfile;
            var reader = this.GetSimpleLogReader(jbossLog, null);
            this.Iterate(reader);
        }

        [Test]
        public void FindsExpectedEventInLogfile()
        {
            var jbossLog = TestConstants.EmbeddedJbossLogfile;
            var jsonDefinition = TestConstants.EmbeddedJsonDefinitionsFileForJbossLog;
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
