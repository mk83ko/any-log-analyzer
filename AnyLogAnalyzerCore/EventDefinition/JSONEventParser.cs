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

        private DefinitionProvider definitionProvider;

        /// <summary>
        /// Constructor to initialize a <c>JsonEventParser</c> object. Please note that given <paramref name="jsonUri"/> must point to a valid JSON file.
        /// </summary>
        /// <param name="jsonUri">File URI of a JSON formatted definition file for logfile events.</param>
        public JsonEventParser(string jsonUri)
        {
            this.InitializeDefinitions(jsonUri);
        }


        /// <summary>
        /// Constructor for initializing a <c>JsonEventParser</c> object with an already initialized <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="jsonFile"></param>
        public JsonEventParser(StreamReader jsonFile)
        {
            this.ReadJsonDefinitions(jsonFile);
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
            var match = false;
            foreach (var def in this.definitionProvider.Definitions)
            {
                foreach (var regex in def.DetectionPatterns)
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

        private void InitializeDefinitions(string jsonUri)
        {
            if (!FilesystemIoHelper.FileExists(jsonUri))
            {
                throw new BadConfigurationException("can't access definition file: " + jsonUri, jsonUri);
            }
            this.ReadJsonDefinitions(FilesystemIoHelper.GetStreamReader(jsonUri));
        }

        private void ReadJsonDefinitions(StreamReader jsonFile)
        {
            if (jsonFile == null)
                throw new BadConfigurationException("eventDefinitions", "null");

            try
            {
                this.definitionProvider = JsonConvert.DeserializeObject<DefinitionProvider>(jsonFile.ReadToEnd());
            }
            catch (JsonReaderException jre)
            {
                string message = "Can't read JSON definition file. Exception was thrown while parsing file:\n" +
                                 jre.Message;
                throw new BadConfigurationException(message);
            }
        }

        private LogEvent CreateEvent(DefinitionElement definition, Timestamp timestamp, LogElement element)
        {
            var logEvent = new LogEvent(definition.Category, element);
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
                if (!String.IsNullOrEmpty(timestamp.Pattern) && !String.IsNullOrEmpty(timestamp.Format))
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
