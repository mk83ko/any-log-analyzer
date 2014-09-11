using Mkko.AnyLogAnalyzerData;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public class JSONEventParser : IEventParser
    {
        public string DefinitionFile { get; set; }

        private DefinitionProvider definitionProvider;

        public JSONEventParser(string jsonUri)
        {
            this.DefinitionFile = jsonUri;
            this.initializeDefinitions();

            foreach (DefinitionElement def in definitionProvider.Definitions)
            {
                Console.WriteLine(def.ToString());
            }
        }

        public bool GetEvent(LogElement element, out List<LogEvent> events)
        {
            events = new List<LogEvent>();
            bool match = false;
            foreach (DefinitionElement def in this.definitionProvider.Definitions)
            {
                foreach (string regex in def.DetectionPatterns)
                {
                    List<string> matches = new List<string>();
                    if (StringHelper.TryGetMatch(element.LogMessage, regex, out matches))
                    {
                        LogEvent logEvent = new LogEvent(def.Category, element);
                        events.Add(logEvent);
                        match = true;
                        break;
                    }
                }
            }
            return match;
        }

        private void initializeDefinitions()
        {
            if (!FilesystemIOHelper.fileExists(this.DefinitionFile))
            {
                throw new FileNotFoundException("can't access specified file", this.DefinitionFile);
            }
            this.readJsonDefinitions();
        }

        private void readJsonDefinitions()
        {
            try
            {
                FileInfo file = FilesystemIOHelper.getFileInfo(this.DefinitionFile);
                string jsonAsString = file.OpenText().ReadToEnd();
                this.definitionProvider = JsonConvert.DeserializeObject<DefinitionProvider>(jsonAsString);
            }
            catch (JsonReaderException jex) { throw jex; }
        }

        private bool hasMatch(bool isRegex, string pattern, string line)
        {

            if (isRegex)
            {
                Regex regex = new Regex(pattern);
                return regex.IsMatch(line);
            }
            return line.Contains(pattern);
        }
    }
}
