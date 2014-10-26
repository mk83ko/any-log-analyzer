using System.Collections.Generic;
using Mkko.EventDefinition;

namespace Mkko.LogFileReader
{
    /// <summary>
    /// this interfaces is used to fetch defined events from a log file
    /// </summary>
    public interface ILogFileReader 
    {
        /// <summary>
        /// returns an iterator for defined events found in the specified log file
        /// </summary>
        /// <returns>iterator containing <c>LogEvent</c> elements</returns>
        IEnumerable<LogEvent> GetEventIterator();

        /// <summary>
        /// identifier for the logfile to be analyzed (e.g. the URI of a text file).
        /// </summary>
        string Logfile { get; set; }

        /// <summary>
        /// the <c>EventDifinition</c> instance to be used for populating the iterator
        /// </summary>
        IEventParser EventDefinition { get; set; }
    }
}
