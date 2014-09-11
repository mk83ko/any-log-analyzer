using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerData
{
    public class LogElement
    {
        public string LogMessage { get; set; }
        public int LineNumber { get; set; }

        public LogElement(string line, int lineNumber)
        {
            LogMessage = line;
            LineNumber = lineNumber;
        }
    }
}
