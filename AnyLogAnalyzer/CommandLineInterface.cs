using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using Mkko.EventDefinition;
using Mkko.LogFileReader;
using Mkko.ReportGenerator;
using Newtonsoft.Json;

namespace Mkko
{
    public class CommandLineInterface
    {
        private ILogFileReader reader;
        private IReportGenerator generator;

        public static int Main(string[] args)
        {
            var arguments = new CliArguments();
            if (Parser.Default.ParseArguments(args, arguments))
            {

                var cli = new CommandLineInterface();

                try
                {
                    cli.Initialize(arguments);
                    cli.generator.CreateReport(cli.reader.GetEventIterator());

                    if (arguments.NumberOfEventsAsReturnCode)
                    {
                        return cli.reader.GetEventIterator().Count();
                    }
                    return (int) CliReturnCodes.ExecutionSuccessful;
                }
                catch (FileNotFoundException fnf)
                {
                    Console.WriteLine(fnf.Message);
                    return (int) CliReturnCodes.FileNotFound;
                }
                catch (JsonReaderException jre)
                {
                    Console.WriteLine(jre.Message);
                }
            }
            else
            {
                Console.WriteLine("not all mandatory parameters were specified. use --help for further information.");
                return (int) CliReturnCodes.MissingParameters;
            }

            return (int)CliReturnCodes.UnhandledException;
        }

        private void Initialize(CliArguments arguments)
        {
            IEventParser definitions = new JsonEventParser(arguments.Definitions);
            reader = new SimpleLogReader(arguments.Logfile) { EventDefinition = definitions };

            switch (arguments.Format)
            {
                case "stdout":
                    generator = new ConsolePrinter();
                    break;
                case "html":
                    this.InitializeHtmlReportGenerator(arguments);
                    break;
                default:
                    generator = new ConsolePrinter();
                    break;
            }
        }

        private void InitializeHtmlReportGenerator(CliArguments arguments){
            
            generator = new HtmlReportGenerator()
            {
                LogFile = arguments.Logfile, 
                FileName = arguments.Output,
                TableColumns = arguments.Columns
            };
        }

        private SortedSet<LogEvent> GetEvents()
        {
            var events = new SortedSet<LogEvent>();
            foreach (LogEvent logEvent in reader.GetEventIterator())
            {
                events.Add(logEvent);
            }

            return events;
        }
    }
}
