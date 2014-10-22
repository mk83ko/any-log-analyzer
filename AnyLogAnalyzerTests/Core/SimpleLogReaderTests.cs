using System;
using Mkko;
using Mkko.EventDefinition;
using Mkko.LogFileReader;

using NUnit.Framework;

namespace AnyLogAnalyzerTests.Core
{
    [TestFixture]
    public class SimpleLogReaderTest
    {
        private readonly string existingLogfile;
        private readonly string existingDefinitionsFile;

        public SimpleLogReaderTest()
        {
            this.existingLogfile = System.Reflection.Assembly.GetEntryAssembly().Location + "\\jboss-boot.log";
            this.existingDefinitionsFile = System.Reflection.Assembly.GetEntryAssembly().Location + "\\jboss.json";
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void NotExistingFileThrowsFileNotFoundException()
        {
            var reader = this.getSimpleLogReader(System.Reflection.Assembly.GetEntryAssembly().Location + "this-file-wont.exist", this.existingDefinitionsFile);
            this.iterate(reader);
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void LogfileUriNotSpecified()
        {
            var reader = this.getSimpleLogReader(null, this.existingDefinitionsFile);
            this.iterate(reader);
        }

        [Test]
        [ExpectedException(typeof(BadConfigurationException))]
        public void DefinitionsNotSpecified()
        {
            var reader = this.getSimpleLogReader(this.existingLogfile, null);
            this.iterate(reader);
        }

        private void iterate(ILogFileReader reader)
        {
            foreach (var logEvent in reader.GetEventIterator())
            {
                Console.WriteLine(logEvent.Category);
            }
        }

        private SimpleLogReader getSimpleLogReader(string logfile, string definitions)
        {
            var reader = new SimpleLogReader(logfile) {EventDefinition = new JsonEventParser(definitions)};

            return reader;
        }
    }
}
