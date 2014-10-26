using System;
using System.Collections.Generic;
using System.Linq;

namespace Mkko
{
    /// <summary>
    /// A <c>DefinitionElement</c> is used to determine if a given <see cref="LogElement"/> contains an event of interest to the user.
    /// </summary>
    public class DefinitionElement
    {
        /// <summary>
        /// This property holds an array of <c>string</c>s representing the patterns for the detection of events.
        /// </summary>
        public string[] DetectionPatterns { get; set; }
        /// <summary>
        /// This property holds a <c>string</c> denominating the event's category.
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// This property triggers the detection patterns to be treated as regular expressions if set to <c>true</c>.
        /// </summary>
        public bool IsRegex { get; set; }
        /// <summary>
        /// This property holds a <see cref="Dictionary{TKey,TValue}"/> of key-value pairs for additional metadata that should be extracted from a given <see cref="LogElement"/>.
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <c>string</c>s containing all keys in <c>Metadata</c>.
        /// </summary>
        /// <returns><see cref="List{T}"/> of <c>string</c>s containing all keys in <c>Metadata</c></returns>
        public List<string> GetMetadataKeys()
        {
            if (this.Metadata != null)
            {
                return this.Metadata.Keys.ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// Returns <c>true</c> if a <c>string</c> value for a given key exists. The queried value is returned by an out parameter.
        /// </summary>
        /// <param name="key">The metadata's key to be returned.</param>
        /// <param name="value">The <c>key</c>'s value if exits or empty string else. Please note that this is an out parameter!</param>
        /// <returns><c>true</c> if metada for <c>key</c> is available or false else.</returns>
        public bool GetMetadataPattern(string key, out string value)
        {
            if (this.Metadata != null)
            {
                return this.Metadata.TryGetValue(key, out value); 
            }
            value = "";
            return false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string output = Category + ": \r\n";
            foreach (string pattern in DetectionPatterns){
                output += "\t" + pattern + "\r\n";
            }
            return output;
        }
    }
}
