using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public class ConsolePrinter : IReportGenerator
    {
        public void CreateReport(SortedSet<LogEvent> events)
        {
            foreach (LogEvent logEvent in events)
            {
                string output = logEvent.Category + " (" + logEvent.Timestamp.ToString() + ", line " + logEvent.Element.LineNumber + "):\r\n";
                foreach (string key in logEvent.GetMetadataKeys())
                {
                    output += "\t*" + key + ": ";
                    List<string> values = new List<string>();
                    logEvent.getMetadata(key, out values);
                    foreach (string value in values)
                    {
                        output += value + ",";
                    }
                    output = output.Substring(0, output.Length - 2) + "\r\n";
                }

                output += "\t(message: " + logEvent.Element.LogMessage + ")";
                Console.WriteLine(output);
            }
        }
    }
}
