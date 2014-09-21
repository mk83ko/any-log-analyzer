using Mkko.AnyLogAnalyzerData;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
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
                        events.Add(this.CreateEvent(def, this.definitionProvider.Timestamp, element));
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

        private LogEvent CreateEvent(DefinitionElement definition, Timestamp timestamp, LogElement element)
        {
            LogEvent logEvent = new LogEvent(definition.Category, element);
            foreach (string key in definition.GetMetadataKeys())
            {
                string regex = "";
                if (definition.Metadata.TryGetValue(key, out regex))
                {
                    List<string> matches = new List<string>();
                    if (StringHelper.TryGetMatch(element.LogMessage, regex, out matches)){
                        logEvent.AddMetadata(key, matches);
                    }
                }
                if (timestamp.Pattern != null && timestamp.Format != null)
                {
                    List<string> rawTimestamp = new List<string>();
                    if (StringHelper.TryGetMatch(element.LogMessage, timestamp.Pattern, out rawTimestamp))
                    {
                        DateTime timestampObject = DateTime.ParseExact(rawTimestamp.ElementAt<string>(0), timestamp.Format, CultureInfo.InvariantCulture);
                        logEvent.Timestamp = timestampObject;
                    }
                }
            }
            return logEvent;
        }
    }
}
