using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;

namespace Mkko.ReportGenerator
{
    public class HtmlReportGenerator : IReportGenerator
    {
        /// <summary>
        /// This property holds the URI to the output file.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// This property holds the URI to the analyzed logfile.
        /// </summary>
        public string LogFile { get; set; }
        /// <summary>
        /// This property defines the columns of the report to be generated from the logfile.
        /// </summary>
        public string[] TableColumns { get; set; }

        private const string HtmlTemplate = "Mkko.res.html.template.html";
        private const string LogEventTableId = "log-event-table";
        private HtmlDocument document;
        private HtmlNodeCollection table;
        private string rowStructure;

        /// <summary>
        /// Creates a HTML report for a given set of <paramref name="events"/>.
        /// </summary>
        /// <param name="events"><see cref="SortedSet{T}"/> of <see cref="LogEvent"/>s found in a logfile.</param>
        public void CreateReport(SortedSet<LogEvent> events)
        {
            this.createRowStructure();
            this.LoadHtmlTemplate();
            this.AddTableHeaders();
            
            foreach (var singleEvent in events)
                this.AddRow(singleEvent);

            this.DeployHtmlReport();
        }

        private void createRowStructure()
        {
            var rowStructure = new StringBuilder("<tr>");
            foreach (var column in this.TableColumns)
                rowStructure.Append("<td>%" + column + "%</td>");

            rowStructure.Append("</tr>");
            this.rowStructure = rowStructure.ToString();
        }

        private void LoadHtmlTemplate()
        {
            this.document = new HtmlDocument();
            this.document.Load( Assembly.GetExecutingAssembly().GetManifestResourceStream(HtmlTemplate) );

            this.table = new HtmlNodeCollection(document.GetElementbyId(LogEventTableId));
        }

        private void AddTableHeaders()
        {
            var header = new StringBuilder(this.rowStructure);
            foreach (var column in this.TableColumns)
                header = header.Replace("<td>%" + column + "%</td>", "<th>" + column + "</th>");

            table.Append(document.CreateTextNode(header.ToString()));
        }

        private void AddRow(LogEvent singleEvent)
        {
            var row = new StringBuilder(rowStructure);
            var metadataKeys = singleEvent.GetMetadataKeys();
            row = row.Replace("<td>%category%</td>", "<td>" + singleEvent.Category + "</td>");
            row = row.Replace("<td>%timestamp%</td>", "<td>" + singleEvent.Timestamp + "</td>");
            row = row.Replace("<td>%linenumber%</td>", "<td>" + singleEvent.Element.LineNumber + "</td>");

            foreach (string key in metadataKeys)
            {
                var values = new List<string>();
                var meta = new StringBuilder();
                if (singleEvent.GetMetadata(key, out values))
                {
                    foreach (string value in values)
                        meta.Append(value + "<br>");
                }
                row = row.Replace("<td>%" + key + "%</td>", "<td>" + meta.ToString() + "</td>");
            }

            row = row.Replace("<td>%element%</td>", "<td>" + singleEvent.Element.LogMessage + "</td>");
            table.Append(document.CreateTextNode(row.ToString()));
        }

        private void DeployHtmlReport()
        {
            this.document.GetElementbyId(LogEventTableId).AppendChildren(table);
            this.document.Save(new StreamWriter(FileName));
        }
    }
}
