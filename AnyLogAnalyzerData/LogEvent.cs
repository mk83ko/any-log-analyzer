using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerData
{
    public class LogEvent : IComparable<LogEvent>
    {
        private string eventCategory;
        private string eventId;
        private LogElement logElement;
        private IDictionary<String, String> metadata;

        public LogEvent(string category, LogElement element){

            this.eventCategory = category;
            this.logElement = element;
            this.metadata = new Dictionary<String, String>();
            this.setId();
        }

        public void AddMetadata(string key, string value)
        {
            if (key != null) { this.metadata.Add(key, value); }
        }

        public int CompareTo(LogEvent other)
        {
            if (!this.eventCategory.Equals(other.eventCategory))
            {
                return this.eventCategory.CompareTo(other.eventCategory);
            }
            else
            {
                if (!this.logElement.LogMessage.Equals(other.logElement.LogMessage))
                {
                    return this.logElement.LogMessage.CompareTo(other.logElement.LogMessage);
                }
            }
            return this.logElement.LineNumber.CompareTo(other.logElement.LineNumber);
        }

        public override string ToString()
        {
            return "[" + this.eventCategory + " at line number " + this.logElement.LineNumber + ": " + this.logElement.LogMessage + "]";
        }

        private void setId()
        {
            this.eventId = this.logElement.LogMessage;
        }
    }
}
