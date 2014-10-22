using System.Collections.Generic;

namespace Mkko.EventDefinition
{
    /// <summary>
    /// provides functionality to transform a single element in a log file into a log event
    /// </summary>
    public interface IEventParser
    {
        /// <summary>
        /// returns any events defined for a given <c>LogElement</c>
        /// </summary>
        /// <param name="element">a single <c>LogElement</c> of a log file</param>
        /// <param name="events">contains a list of <c>LogEvent</c>s found in the provided <c>LogElement</c></param>
        /// <remarks>is an <c>out</c> parameter</remarks>
        /// <returns><c>true</c> if a provided <c>LogElement</c> matches any event definition or false otherwise</returns>
        bool GetEvent(LogElement element, out List<LogEvent> events);
    }
}
