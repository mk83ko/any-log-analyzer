using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mkko.AnyLogAnalyzerCore
{
    public interface IEventParser
    {
        bool HasMatch(LogElement logElement);
        LogEvent GetEvent(LogElement logElement);
    }
}
