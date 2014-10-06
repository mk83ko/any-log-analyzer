﻿using CommandLine;
using CommandLine.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko
{
    class CliArguments
    {
        [Option('l', "logifle", Required = true, HelpText = "log file to read.")]
        public string Logfile { get; set; }

        [Option('d', "definitions", Required = true, HelpText = "file containing the event definitions.")]
        public string Definitions { get; set; }

        [Option('f', "format", DefaultValue = "text", Required = false, HelpText = "output format for the results (text or html). deafult format is text")]
        public string Format { get; set; }

        [Option('o', "output", DefaultValue = "stdout", Required = false, HelpText = "destination for output (stdout or file). default is stdout.")]
        public string Output { get; set; }

        [Option('r', "returnCode", DefaultValue = false, Required = false, HelpText = "if set to true, the application returns the number of events as return code to the caller.")]
        public bool NumberOfEventsAsReturnCode { get; set; }
    }
}