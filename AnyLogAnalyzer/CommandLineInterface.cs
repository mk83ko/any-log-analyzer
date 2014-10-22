using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using Mkko.EventDefinition;
using Mkko.LogFileReader;
using Mkko.ReportGenerator;
using Newtonsoft.Json;

namespace Mkko
{
    class CommandLineInterface
    {
        private ILogFileReader reader;
        private IReportGenerator generator;

        static int Main(string[] args)
        {
            CliArguments arguments = new CliArguments();
            if (Parser.Default.ParseArguments(args, arguments)){
                
                CommandLineInterface cli = new CommandLineInterface();

                try
                {
                    cli.Initialize(arguments);
                    SortedSet<LogEvent> events = cli.GetEvents();
                    cli.generator.CreateReport(events);

                    if (arguments.NumberOfEventsAsReturnCode)
                    {
                        return events.Count;
                    }
                    return 0;
                }
                catch (FileNotFoundException fnf)
                {
                    Console.WriteLine(fnf.Message);
                }
                catch (JsonReaderException jre)
                {
                    Console.WriteLine(jre.Message);
                }
            }
            PrintUsage();
            return 1;
        }

        private void Initialize(CliArguments arguments)
        {
            try
            {
                IEventParser definitions = new JsonEventParser(arguments.Definitions);

                reader = new SimpleLogReader(arguments.Logfile);
                reader.EventDefinition = definitions;

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
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void InitializeHtmlReportGenerator(CliArguments arguments){
            
            generator = new HtmlReportGenerator(arguments.Output) { LogFile = arguments.Logfile };
        }

        private SortedSet<LogEvent> GetEvents()
        {
            SortedSet<LogEvent> events = new SortedSet<LogEvent>();
            foreach (LogEvent logEvent in reader.GetEventIterator())
            {
                events.Add(logEvent);
            }

            return events;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("print usage here");
        }
    }
}
