using Mkko.AnyLogAnalyzerCore;
using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzer
{
    class CommandLineInterface
    {
        private string[] parameters = { "logfile", "definitions", "output" };
        private ILogFileReader reader = null;
        private IReportGenerator generator = null;

        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length % 2 != 0)
            {
                printUsage();
            }
            else 
            { 
                CommandLineInterface cli = new CommandLineInterface();
                cli.initialize();
            }
        }

        private void initialize()
        {
            Dictionary<string, string> arguments = this.getCommandLineArguments();
            try
            {
                string logfile = "";  
                string defFile = "";
                string report = "";
                arguments.TryGetValue("logfile", out logfile);
                arguments.TryGetValue("definitions", out defFile);
                arguments.TryGetValue("output", out report);

                reader = new SimpleLogReader(logfile);
                reader.EventDefinition = new JSONEventParser(defFile);

                SortedSet<LogEvent> events = new SortedSet<LogEvent>();

                foreach (LogEvent logEvent in reader.getEventIterator())
                {
                    events.Add(logEvent);
                }

                HtmlReportGenerator generator = new HtmlReportGenerator();
                generator.FileName = report;
                generator.LogFile = logfile;
                generator.CreateReport(events);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CommandLineInterface.printUsage();
            }
        }

        private Dictionary<string, string> getCommandLineArguments()
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i = i + 2)
            {
                arguments.Add(args[i], args[i + 1]);
            }
            return arguments;
        }

        private static void printUsage()
        {
            Console.WriteLine("print usage here");
        }
    }
}
