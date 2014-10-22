using System;
using System.Collections.Generic;
using System.Linq;

namespace Mkko
{
    /// <summary>
    /// Objects of this class represents events of interest in a logfile.
    /// </summary>
    public class LogEvent : IComparable<LogEvent>
    {
// ReSharper disable CSharpWarnings::CS1591
        public DateTime Timestamp { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
        public LogElement Element { get; set; }
// ReSharper restore CSharpWarnings::CS1591

        private IDictionary<String, List<string>> metadata;

        public LogEvent(string category, LogElement element){

            this.Category = category;
            this.Element = element;
            this.metadata = new Dictionary<String, List<string>>();
            this.SetId();
        }

        public void AddMetadata(string key, List<string> value)
        {
            if (key != null) { this.metadata.Add(key, value); }
        }

        public List<string> GetMetadataKeys()
        {
            if (this.metadata != null)
            {
                return this.metadata.Keys.ToList();
            }
            return new List<string>();
        }

        public bool GetMetadata(string key, out List<string> value)
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
            if (!this.Element.LogMessage.Equals(other.Element.LogMessage))
            {
                return this.Element.LogMessage.CompareTo(other.Element.LogMessage);
            }
            return this.Element.LineNumber.CompareTo(other.Element.LineNumber);
        }

        public override string ToString()
        {
            var output = this.Timestamp + "> " + this.Category + " at line number " + this.Element.LineNumber + ":\r\n";
            foreach (var key in this.metadata.Keys)
            {
                output += "\t" + key + ": ";
                List<string> values;
                this.metadata.TryGetValue(key, out values);
                if (values != null)
                    foreach (var value in values)
                    {
                        output += value + ", ";
                    }
            }
            return output;
        }

        private void SetId()
        {
            this.Id = this.Element.LogMessage;
        }
    }
}
