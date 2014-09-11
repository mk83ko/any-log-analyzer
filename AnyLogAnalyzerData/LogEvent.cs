using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerData
{
    public class LogEvent : IComparable<LogEvent>
    {
        public DateTime timestamp { get; set; }

        private string eventCategory;
        private string eventId;
        private LogElement logElement;
        private IDictionary<String, List<string>> metadata;

        public LogEvent(string category, LogElement element){

            this.eventCategory = category;
            this.logElement = element;
            this.metadata = new Dictionary<String, List<string>>();
            this.setId();
        }

        public void AddMetadata(string key, List<string> value)
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
            string output = this.timestamp.ToString() + "> " + this.eventCategory + " at line number " + this.logElement.LineNumber + ":\r\n";
            foreach (string key in this.metadata.Keys)
            {
                output += "\t" + key + ": ";
                List<string> values = new List<String>();
                this.metadata.TryGetValue(key, out values);
                foreach (string value in values)
                {
                    output += value + ", ";
                }
            }
            return output;
        }

        private void setId()
        {
            this.eventId = this.logElement.LogMessage;
        }
    }
}
