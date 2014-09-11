﻿using Mkko.AnyLogAnalyzerCore;
using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileuri = @"D:/work/bamboo_example.log";
            string jsonuri = @"D:/work/definitions_example.json";
            ILogFileReader logfileReader = new SimpleLogReader(fileuri);
            logfileReader.EventDefinition = new JSONEventParser(jsonuri);

            SortedSet<LogEvent> events = new SortedSet<LogEvent>();

            foreach (LogEvent logEvent in logfileReader.getEventIterator())
            {
                events.Add(logEvent);
            }

            foreach (LogEvent logEvent in events)
            {
                Console.WriteLine(logEvent.ToString());
            }
            Console.ReadLine();
        }
    }
}