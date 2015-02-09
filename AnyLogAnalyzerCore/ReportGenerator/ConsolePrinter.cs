using System;
using System.Collections.Generic;
using System.Linq;

namespace Mkko.ReportGenerator
{
    /// <summary>
    /// This implementation of <see cref="IReportGenerator"/> prints all events to the console./>
    /// </summary>
    public class ConsolePrinter : IReportGenerator
    {
        /// <summary>
        /// Prints the supplied <c>LogEvent</c>s to console.
        /// </summary>
        /// <param name="events"><see cref="SortedSet{T}"/> of <see cref="LogEvent"/>s.</param>
        public void CreateReport(IEnumerable<LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                var output = logEvent.Category + " (" + logEvent.Timestamp + ", line " + logEvent.Element.LineNumber + "):\r\n";
                foreach (var key in logEvent.GetMetadataKeys())
                {
                    output += "\t*" + key + ": ";
                    List<string> values;
                    logEvent.GetMetadata(key, out values);
                    output = values.Aggregate(output, (current, value) => current + (value + ","));
                    output = output.Substring(0, output.Length - 2) + "\r\n";
                }

                output += "\t(message: " + logEvent.Element.LogMessage + ")";
                Console.WriteLine(output);
            }
        }
    }
}
