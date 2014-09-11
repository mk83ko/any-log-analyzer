using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko
{
    public class DefinitionElement
    {
        public string[] DetectionPatterns { get; set; }
        public string Category { get; set; }
        public bool IsRegex { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        public bool hasPatternForMetadata(string key)
        {
            return Metadata.ContainsKey(key);
        }

        public bool getMetadataPattern(string key, out string value)
        {
            return this.Metadata.TryGetValue(key, out value);
        }

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
