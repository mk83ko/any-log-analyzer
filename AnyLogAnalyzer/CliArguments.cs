using System;
using CommandLine;
using CommandLine.Text;

namespace Mkko
{
    class CliArguments
    {
        [Option('l', "logfile", Required = false, HelpText = "log file to read.")]
        public string Logfile { get; set; }

        [Option('d', "definitions", Required = false, HelpText = "file containing the event definitions.")]
        public string Definitions { get; set; }

        [Option('f', "format", Required = false, HelpText = "output format for the results (text or html). default format is text")]
        public string Format { get; set; }

        [Option('o', "output", Required = false, HelpText = "destination for output (stdout or file). default is stdout.")]
        public string Output { get; set; }

        [Option('r', "returnCode", Required = false, DefaultValue = false, HelpText = "if set to true, the application returns the number of events as return code to the caller.")]
        public bool NumberOfEventsAsReturnCode { get; set; }

        [Option('c', "configuration", Required = false, HelpText = "file containing the configuration.")]
        public string Configuration { get; set; }

        [OptionArray('v', "valuesToDisplay", Required = false, HelpText = "defines the columns of the report.")] 
        public string[] Columns { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public Type GetCliArgument(string key, out object value)
        {
            switch (key)
            {
                case ConfigParameterProvider.Configuration:
                    value = this.Configuration;
                    return this.Configuration.GetType();
                case ConfigParameterProvider.Definitions:
                    value = this.Definitions;
                    return this.Definitions.GetType();
                case ConfigParameterProvider.Format:
                    value = this.Format;
                    return this.Format.GetType();
                case ConfigParameterProvider.Logfile:
                    value = this.Logfile;
                    return this.Logfile.GetType();
                case ConfigParameterProvider.Output:
                    value = this.Output;
                    return this.Output.GetType();
                case ConfigParameterProvider.ReturnCode:
                    value = this.NumberOfEventsAsReturnCode;
                    return this.NumberOfEventsAsReturnCode.GetType();
                case ConfigParameterProvider.ValuesToDisplay:
                    value = this.Columns;
                    return this.Columns.GetType();
                default:
                    value = null;
                    return null;
            }
        }
    }
}
