using System;
using Mkko;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Mkko.AnyLogAnalyzerTests.CliApp
{
    [TestClass]
    public class CommandLineInterfaceTests
    {
        [TestMethod]
        public void AppReturnsFileNotFoundReturnCodeIfFileIsMissing()
        {
            string[] args = {"-l", "not/existing/log.file", "-d", "not/existing/definitions.file"};
            var returncode = CommandLineInterface.Main(args);

            Assert.AreEqual((int)CliReturnCodes.FileNotFound, returncode);
        }

        [TestMethod]
        public void AppReturnsMissingParametersReturnCodeIfMandatoryParametersAreMissing()
        {
            string[] args = { "-l", TestConstants.EmbeddedJbossLogfile };
            var returncode = CommandLineInterface.Main(args);

            Assert.AreEqual((int)CliReturnCodes.MissingParameters, returncode);
        }
    }
}
