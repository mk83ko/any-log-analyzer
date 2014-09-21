using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public interface IReportGenerator
    {
        void CreateReport(SortedSet<LogEvent> events);
    }
}
