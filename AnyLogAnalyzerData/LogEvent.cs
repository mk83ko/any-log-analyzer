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
        public LogElement Element { get; set; }
// ReSharper restore CSharpWarnings::CS1591

        private IDictionary<String, List<string>> metadata;

        /// <summary>
        /// Constructor to create an instance of <see cref="LogEvent"/>.
        /// </summary>
        /// <param name="category">string value for this instance's <see cref="Category"/>.</param>
        /// <param name="element"><see cref="LogElement"/> that is root of this <see cref="LogEvent"/>.</param>
        public LogEvent(string category, LogElement element){

            this.Category = category;
            this.Element = element;
            this.metadata = new Dictionary<String, List<string>>();
        }

        /// <summary>
        /// <see cref="LogEvent"/>s can have arbitrary metadata elements. Call this method in order to add another metadata element for this instance.
        /// </summary>
        /// <param name="key"><c>string</c> value for the metadata's key.</param>
        /// <param name="value">A <see cref="List{T}"/> of <c>string</c>s representing the metadata's values.</param>
        public void AddMetadata(string key, List<string> value)
        {
            if (key != null) { this.metadata.Add(key, value); }
        }

        /// <summary>
        /// Returns a list of all metadata keys that exist for this instance.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <c>string</c>s representing all metadata's keys.</returns>
        public List<string> GetMetadataKeys()
        {
            if (this.metadata != null)
            {
                return this.metadata.Keys.ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// Returns the values stored for a given metadata element.
        /// </summary>
        /// <param name="key">The metadata's key.</param>
        /// <param name="value">A <see cref="List{T}"/> of <c>string</c>s representing all values for <paramref name="key"/>.
        ///     <remarks>this is a out parameter.</remarks>
        /// </param>
        /// <returns><c>true</c> if this instances has metadata for <paramref name="key"/> or <c>false</c> else.</returns>
        public bool GetMetadata(string key, out List<string> value)
        {
            if (this.metadata != null)
            {
                return this.metadata.TryGetValue(key, out value);
            }
            value = new List<string>();
            return false;
        }

        /// <summary>
        /// Ordering of <c>LogEvent</c>s is defined first by <see cref="Category"/> (alphabetically) and if equal second by line number (ascending).
        /// </summary>
        /// <param name="other"><c>LogEvent</c> that should be compared to this instance.</param>
        /// <returns>negative <c>int</c> if this instance precedes <c>other</c>, positive <c>int</c> if this element follows <c>other</c> and <c>0</c> else.</returns>
        public int CompareTo(LogEvent other)
        {
            if (!this.Category.Equals(other.Category))
            {
                return this.Category.CompareTo(other.Category);
            }
            return this.Element.LineNumber.CompareTo(other.Element.LineNumber);
        }

        /// <summary>
        /// Returns a human readable representation of this <c>LogEvent</c>.
        /// </summary>
        /// <returns>Representation of this instance.</returns>
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
    }
}
