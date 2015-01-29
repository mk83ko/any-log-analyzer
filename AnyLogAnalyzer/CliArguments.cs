using CommandLine;
using CommandLine.Text;

namespace Mkko
{
    class CliArguments
    {
        [Option('l', "logfile", Required = true, HelpText = "log file to read.")]
        public string Logfile { get; set; }

        [Option('d', "definitions", Required = true, HelpText = "file containing the event definitions.")]
        public string Definitions { get; set; }

        [Option('f', "format", DefaultValue = "text", Required = false, HelpText = "output format for the results (text or html). default format is text")]
        public string Format { get; set; }

        [Option('o', "output", DefaultValue = "stdout", Required = false, HelpText = "destination for output (stdout or file). default is stdout.")]
        public string Output { get; set; }

        [Option('r', "returnCode", DefaultValue = false, Required = false, HelpText = "if set to true, the application returns the number of events as return code to the caller.")]
        public bool NumberOfEventsAsReturnCode { get; set; }

        [OptionArray('c', "columns", DefaultValue = new string[] {"category", "timestamp", "linenumber", "element"}, Required = false, HelpText = "defines the columns of the report.")] 
        public string[] Columns { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
