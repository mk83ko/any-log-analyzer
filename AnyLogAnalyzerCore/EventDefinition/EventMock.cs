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

        public bool GetEvent(LogElement element, out List<LogEvent> events)
        {
            bool match = false;
            events = new List<LogEvent>();
            foreach (string definition in definitions)
            {
                if (element.LogMessage.Contains(definition))
                {
                    match = true;
                    events.Add(new LogEvent("mock-event", element));
                }
            }
            return match;
        }
    }
}
