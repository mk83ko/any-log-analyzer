using CommandLine;

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
        private ILogFileReader reader = null;
        private IReportGenerator generator = null;

        static int Main(string[] args)
        {
            CliArguments arguments = new CliArguments();
            if (Parser.Default.ParseArguments(args, arguments)){
                
                CommandLineInterface cli = new CommandLineInterface();
                cli.initialize(arguments);
                SortedSet<LogEvent> events = cli.getEvents();
                cli.generator.CreateReport(events);

                if (arguments.NumberOfEventsAsReturnCode)
                {
                    return events.Count;
                }
                return 0;
            }
            printUsage();
            return 1;
        }

        private void initialize(CliArguments arguments)
        {
            try
            {
                IEventParser definitions = new JSONEventParser(arguments.Definitions);

                reader = new SimpleLogReader(arguments.Logfile);
                reader.EventDefinition = definitions;

                switch (arguments.Format)
                {
                    case "stdout":
                        generator = new ConsolePrinter();
                        break;
                    case "html":
                        generator = this.getHtmlReportGenerator(arguments);
                        break;
                    default:
                        generator = new ConsolePrinter();
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private HtmlReportGenerator getHtmlReportGenerator(CliArguments arguments){
            
            HtmlReportGenerator generator = new HtmlReportGenerator(arguments.Output);
            generator.LogFile = arguments.Logfile;
            return generator;
        }

        private SortedSet<LogEvent> getEvents()
        {
            SortedSet<LogEvent> events = new SortedSet<LogEvent>();
            foreach (LogEvent logEvent in reader.getEventIterator())
            {
                events.Add(logEvent);
            }

            return events;
        }

        private static void printUsage()
        {
            Console.WriteLine("print usage here");
        }
    }
}
