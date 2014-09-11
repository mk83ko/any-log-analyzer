using Mkko.AnyLogAnalyzerData;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public class SimpleLogReader : ILogFileReader
    {
        public string Logfile{ get; set; }
        public IEventParser EventDefinition { get; set; }

        private StreamReader streamreader;
        private int currentLine = 0;

        public SimpleLogReader(string logfile)
        {
            this.Logfile = logfile;
        }

        public IEnumerable<LogEvent> getEventIterator()
        {
            if (this.streamreader == null)
            {
                this.initializeFileSteam();
            }

            using (this.streamreader)
            {
                string line = null;
                while ((line = this.streamreader.ReadLine()) != null)
                {
                    this.currentLine++;
                    LogElement element = new LogElement(line, this.currentLine);
                    if (this.EventDefinition.HasMatch(element))
                    {
                        yield return this.EventDefinition.GetEvent(element);
                    }
                }
            }
            this.closeFileStream();
        }

        private void initializeFileSteam()
        {
            if (this.Logfile == null || !FilesystemIOHelper.fileExists(this.Logfile))
            {
                throw new FileNotFoundException("file not found or no URI specified", this.Logfile);
            }

            this.streamreader = (FilesystemIOHelper.getFileInfo(this.Logfile)).OpenText();
        }

        private void closeFileStream()
        {
            this.streamreader.Dispose();
        }
    }
}
