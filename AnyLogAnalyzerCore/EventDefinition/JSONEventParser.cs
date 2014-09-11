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

        private List<DefinitionElement> definitions;

        public JSONEventParser(string jsonUri)
        {
            this.DefinitionFile = jsonUri;
            this.initializeDefinitions();
        }

        public bool HasMatch(LogElement logElement)
        {
            foreach (DefinitionElement definition in definitions)
            {
                foreach (string pattern in definition.Patterns)
                {
                    string literalPattern = StringHelper.ToLiteral(pattern);
                    if (this.hasMatch(definition.IsRegex, pattern, logElement.LogMessage))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public LogEvent GetEvent(LogElement logElement)
        {
            foreach (DefinitionElement definition in definitions)
            {
                foreach (string pattern in definition.Patterns)
                {
                    string literalPattern = StringHelper.ToLiteral(pattern);
                    if (this.hasMatch(definition.IsRegex, pattern, logElement.LogMessage))
                    {
                        return new LogEvent(definition.Category, logElement);
                    }
                }
            }
            return null;
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
                this.definitions = JsonConvert.DeserializeObject<List<DefinitionElement>>(jsonAsString);
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
