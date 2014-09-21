using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    /// <summary>
    /// this interfaces is used to fetch defined events from a log file
    /// </summary>
    public interface ILogFileReader 
    {
        /// <summary>
        /// returns an iterator for definied events found in the specified log file
        /// </summary>
        /// <returns>iterator containing <c>LogEvent</c> elements</returns>
        IEnumerable<LogEvent> getEventIterator();

        string Logfile { get; set; }

        /// <summary>
        /// the <c>EventDifinition</c> instance to be used for populating the iterator
        /// </summary>
        IEventParser EventDefinition { get; set; }
    }
}
