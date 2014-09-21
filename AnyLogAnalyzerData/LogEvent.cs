using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerData
{
    public class LogEvent : IComparable<LogEvent>
    {
        public DateTime Timestamp { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
        public LogElement Element { get; set; }

        private IDictionary<String, List<string>> metadata;

        public LogEvent(string category, LogElement element){

            this.Category = category;
            this.Element = element;
            this.metadata = new Dictionary<String, List<string>>();
            this.setId();
        }

        public void AddMetadata(string key, List<string> value)
        {
            if (key != null) { this.metadata.Add(key, value); }
        }

        public List<string> GetMetadataKeys()
        {
            if (this.metadata != null)
            {
                return this.metadata.Keys.ToList<String>();
            }
            return new List<string>();
        }

        public bool getMetadata(string key, out List<string> value)
        {
            if (this.metadata != null)
            {
                return this.metadata.TryGetValue(key, out value);
            }
            value = new List<string>();
            return false;
        }

        public int CompareTo(LogEvent other)
        {
            if (!this.Category.Equals(other.Category))
            {
                return this.Category.CompareTo(other.Category);
            }
            else
            {
                if (!this.Element.LogMessage.Equals(other.Element.LogMessage))
                {
                    return this.Element.LogMessage.CompareTo(other.Element.LogMessage);
                }
            }
            return this.Element.LineNumber.CompareTo(other.Element.LineNumber);
        }

        public override string ToString()
        {
            string output = this.Timestamp.ToString() + "> " + this.Category + " at line number " + this.Element.LineNumber + ":\r\n";
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
            this.Id = this.Element.LogMessage;
        }
    }
}
