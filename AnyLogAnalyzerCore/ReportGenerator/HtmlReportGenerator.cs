using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;


namespace Mkko.AnyLogAnalyzerCore
{
    public class HtmlReportGenerator : IReportGenerator
    {
        public string FileName { get; set; }
        public string LogFile { get; set; }

        private HtmlTextWriter htmlWriter;

        public HtmlReportGenerator(string output)
        {
            this.FileName = output;
        }

        public void CreateReport(SortedSet<LogEvent> events)
        {
            StreamWriter streamWriter = FilesystemIOHelper.openFileForWriting(this.FileName);
            htmlWriter = new HtmlTextWriter(streamWriter);

            this.writeHead();

            this.writeBeginTag("table", " border=1 width=100%");
            this.writeTableHeader();

            foreach (LogEvent logEvent in events)
            {
                this.writeTableRow(logEvent);
            }

            htmlWriter.WriteEndTag("table");
        }

        private void writeHead()
        {
            string title = "analysis of log file " + this.LogFile;

            this.writeBeginTag("title");
            htmlWriter.Write(title);
            this.writeEndTag("title");

            this.writeBeginTag("h1");
            htmlWriter.Write(title);
            this.writeEndTag("h1");
        }

        private void writeTableHeader()
        {
            string[] headers = { "category", "timestamp", "linenumber", "metadata", "element" };
            
            this.writeBeginTag("tr");
            foreach (string head in headers)
            {
                this.writeBeginTag("td");
                htmlWriter.Write(head);
                this.writeEndTag("td");
            }
            this.writeEndTag("tr");
        }

        private void writeTableRow(LogEvent logEvent)
        {
            List<string> metadataKeys = logEvent.GetMetadataKeys();
            string rowspan = "";
            if (metadataKeys.Count > 1)
            {
                rowspan = " rowspan=" + metadataKeys.Count;
            }

            this.writeBeginTag("tr");

            this.writeBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Category);
            this.writeEndTag("td");

            this.writeBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Timestamp);
            this.writeEndTag("td");

            this.writeBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Element.LineNumber);
            this.writeEndTag("td");

            this.writeBeginTag("td");
            if (metadataKeys.Count > 0)
            {
                string key = metadataKeys.ElementAt<string>(0);
                string value = "";
                List<string> values = new List<string>();
                logEvent.getMetadata(key, out values);
                foreach (string metadata in values)
                {
                    value += "<br>" + metadata;
                }
                htmlWriter.Write(key + ": " + value);
            }
            this.writeEndTag("td");

            this.writeBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Element.LogMessage);
            this.writeEndTag("td");

            this.writeEndTag("tr");

            for (int i = 1; i < metadataKeys.Count; i++)
            {
                this.writeBeginTag("tr");

                this.writeBeginTag("td");
                string key = metadataKeys.ElementAt<string>(i);
                string value = "";
                List<string> values = new List<string>();
                logEvent.getMetadata(key, out values);
                foreach (string metadata in values)
                {
                    value += "<br>" + metadata;
                }
                htmlWriter.Write(key + ": " + value);
                this.writeEndTag("td");

                this.writeEndTag("tr");
            }
        }

        private void writeMetadata(LogEvent logEvent) { }
        

        private void writeBeginTag(string tag, string attributes = "")
        {
            htmlWriter.WriteBeginTag(tag);
            htmlWriter.Write(attributes + HtmlTextWriter.TagRightChar);
        }

        private void writeEndTag(string tag)
        {
            htmlWriter.WriteEndTag(tag);
        }
    }
}
