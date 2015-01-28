using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Mkko.ReportGenerator
{
    /// <summary>
    /// This implementation of <see cref="IReportGenerator"/> creates a HTML report./>
    /// </summary>
    public class SimpleHtmlReportGenerator : IReportGenerator
    {
        /// <summary>
        /// This property holds the URI to the output file.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// This property holds the URI to the analyzed logfile.
        /// </summary>
        public string LogFile { get; set; }

        private HtmlTextWriter htmlWriter;

        /// <summary>
        /// Constructor to be used to initialize a <c>SimpleHtmlReportGenerator</c> object.
        /// </summary>
        /// <param name="output">This string holds the URI for the output report file.</param>
        public SimpleHtmlReportGenerator(string output)
        {
            this.FileName = output;
        }

        /// <summary>
        /// Creates a HTML report for a given set of <paramref name="events"/>.
        /// </summary>
        /// <param name="events"><see cref="SortedSet{T}"/> of <see cref="LogEvent"/>s found in a logfile.</param>
        public void CreateReport(SortedSet<LogEvent> events)
        {
            var streamWriter = FilesystemIoHelper.GetStreamWriter(this.FileName);
            htmlWriter = new HtmlTextWriter(streamWriter);

            this.WriteHead();

            this.WriteBeginTag("table", " border=1 width=100%");
            this.WriteTableHeader();

            foreach (LogEvent logEvent in events)
            {
                this.WriteTableRow(logEvent);
            }

            htmlWriter.WriteEndTag("table");
        }

        private void WriteHead()
        {
            string title = "analysis of log file " + this.LogFile;

            this.WriteBeginTag("title");
            htmlWriter.Write(title);
            this.WriteEndTag("title");

            this.WriteBeginTag("h1");
            htmlWriter.Write(title);
            this.WriteEndTag("h1");
        }

        private void WriteTableHeader()
        {
            string[] headers = { "category", "timestamp", "linenumber", "metadata", "element" };
            
            this.WriteBeginTag("tr");
            foreach (string head in headers)
            {
                this.WriteBeginTag("td");
                htmlWriter.Write(head);
                this.WriteEndTag("td");
            }
            this.WriteEndTag("tr");
        }

        private void WriteTableRow(LogEvent logEvent)
        {
            List<string> metadataKeys = logEvent.GetMetadataKeys();
            string rowspan = "";
            if (metadataKeys.Count > 1)
            {
                rowspan = " rowspan=" + metadataKeys.Count;
            }

            this.WriteBeginTag("tr");

            this.WriteBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Category);
            this.WriteEndTag("td");

            this.WriteBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Timestamp);
            this.WriteEndTag("td");

            this.WriteBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Element.LineNumber);
            this.WriteEndTag("td");

            this.WriteBeginTag("td");
            if (metadataKeys.Count > 0)
            {
                string key = metadataKeys.ElementAt(0);
                string value = "";
                List<string> values;
                logEvent.GetMetadata(key, out values);
                foreach (string metadata in values)
                {
                    value += "<br>" + metadata;
                }
                htmlWriter.Write(key + ": " + value);
            }
            this.WriteEndTag("td");

            this.WriteBeginTag("td", rowspan);
            htmlWriter.Write(logEvent.Element.LogMessage);
            this.WriteEndTag("td");

            this.WriteEndTag("tr");

            for (int i = 1; i < metadataKeys.Count; i++)
            {
                this.WriteBeginTag("tr");

                this.WriteBeginTag("td");
                string key = metadataKeys.ElementAt(i);
                string value = "";
                List<string> values;
                logEvent.GetMetadata(key, out values);
                foreach (string metadata in values)
                {
                    value += "<br>" + metadata;
                }
                htmlWriter.Write(key + ": " + value);
                this.WriteEndTag("td");

                this.WriteEndTag("tr");
            }
        }

        private void WriteMetadata(LogEvent logEvent) { }
        

        private void WriteBeginTag(string tag, string attributes = "")
        {
            htmlWriter.WriteBeginTag(tag);
            htmlWriter.Write(attributes + HtmlTextWriter.TagRightChar);
        }

        private void WriteEndTag(string tag)
        {
            htmlWriter.WriteEndTag(tag);
        }
    }
}
