using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public class EventMock : IEventParser
    {
        private string[] definitions = { "[[WORKER]]", "[[SUBTARGET]]", "[[PUBLICTARGET]]" };


        public bool HasMatch(LogElement logElement)
        {
            foreach (string definition in definitions){
                if (logElement.LogMessage.Contains(definition))
                {
                    return true;
                }
            }
            return false;
        }

        public LogEvent GetEvent(LogElement logElement)
        {
            if (!this.HasMatch(logElement))
            {
                return null;
            }
            return new LogEvent("mockevent", logElement);
        }
    }
}
