using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Mkko.EventDefinition
{
    /// <summary>
    /// This implementation of <see cref="IEventParser"/> reads a event definition file of JSON format.
    /// </summary>
    public class JsonEventParser : IEventParser
    {
        /// <summary>
        /// The URI to the file holding the event definition.
        /// </summary>
        public string DefinitionFile { get; set; }

        private DefinitionProvider definitionProvider;

        /// <summary>
        /// Constructor to initialize a <c>JSONEventParser</c> object. Please note that given <paramref name="jsonUri"/> must point to a valid JSON file.
        /// </summary>
        /// <param name="jsonUri">File URI of a JSON formatted definition file for logfile events.</param>
        public JsonEventParser(string jsonUri)
        {
            this.DefinitionFile = jsonUri;
            this.InitializeDefinitions();

            foreach (DefinitionElement def in definitionProvider.Definitions)
            {
                Console.WriteLine(def.ToString());
            }
        }

        /// <summary>
        /// Parses a single <see cref="LogElement"/> object and returns all <see cref="LogEvent"/>s that match the parsed object.
        /// </summary>
        /// <param name="element">A single <c>LogElement</c> object representing an element of the logfile.</param>
        /// <param name="events">A <c>List</c> of <c>LogEvents</c> that is used to return the matching <c>LogEvents</c> for this parsers event definition.</param>
        /// <returns></returns>
        public bool GetEvent(LogElement element, out List<LogEvent> events)
        {
            events = new List<LogEvent>();
            bool match = false;
            foreach (DefinitionElement def in this.definitionProvider.Definitions)
            {
                foreach (string regex in def.DetectionPatterns)
                {
                    List<string> matches;
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

        private void InitializeDefinitions()
        {
            if (!FilesystemIoHelper.FileExists(this.DefinitionFile))
            {
                throw new FileNotFoundException("can't access definition file: " + this.DefinitionFile, this.DefinitionFile);
            }
            this.ReadJsonDefinitions();
        }

        private void ReadJsonDefinitions()
        {
            try
            {
                FileInfo file = FilesystemIoHelper.GetFileInfo(this.DefinitionFile);
                string jsonAsString = file.OpenText().ReadToEnd();
                this.definitionProvider = JsonConvert.DeserializeObject<DefinitionProvider>(jsonAsString);
            }
            catch (JsonReaderException jre) { throw jre; }
        }

        private LogEvent CreateEvent(DefinitionElement definition, Timestamp timestamp, LogElement element)
        {
            LogEvent logEvent = new LogEvent(definition.Category, element);
            foreach (string key in definition.GetMetadataKeys())
            {
                string regex;
                if (definition.Metadata.TryGetValue(key, out regex))
                {
                    List<string> matches;
                    if (StringHelper.TryGetMatch(element.LogMessage, regex, out matches)){
                        logEvent.AddMetadata(key, matches);
                    }
                }
                if (timestamp.Pattern != null && timestamp.Format != null)
                {
                    List<string> rawTimestamp;
                    if (StringHelper.TryGetMatch(element.LogMessage, timestamp.Pattern, out rawTimestamp))
                    {
                        var timestampObject = DateTime.ParseExact(rawTimestamp.ElementAt(0), timestamp.Format, CultureInfo.InvariantCulture);
                        logEvent.Timestamp = timestampObject;
                    }
                }
            }
            return logEvent;
        }
    }
}
