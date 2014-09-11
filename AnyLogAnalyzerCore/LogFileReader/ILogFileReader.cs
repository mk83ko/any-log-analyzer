using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public interface ILogFileReader 
    {
        IEnumerable<LogEvent> getEventIterator();

        string Logfile { get; set; }

        IEventParser EventDefinition { get; set; }
    }
}
