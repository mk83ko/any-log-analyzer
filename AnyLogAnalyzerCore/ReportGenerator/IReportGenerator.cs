using System.Collections.Generic;

namespace Mkko.ReportGenerator
{
    /// <summary>
    /// an implementation of this interface must transform a <see cref="SortedSet{T}"/> of <see cref="LogEvent"/>s into a report of some kind.
    /// </summary>
    public interface IReportGenerator
    {
        /// <summary>
        /// Creates a report for a given <see cref="SortedSet{T}"/> of <see cref="LogEvent"/>s.
        /// </summary>
        /// <param name="events"></param>
        void CreateReport(SortedSet<LogEvent> events);
    }
}
