using System.Collections.Generic;
using System.IO;
using Mkko.EventDefinition;

namespace Mkko.LogFileReader
{
    /// <summary>
    /// This class is an implementation of the <c>ILogFileReader</c> interface. It reads a logfile line by line.
    /// </summary>
    public class SimpleLogReader : ILogFileReader
    {
        /// <summary>
        /// The file URI of the logfile to be parsed.
        /// </summary>
        public string Logfile{ get; set; }
        /// <summary>
        /// An implementation of <c>IEventParser</c> that holds the appropriate even definitions./>.
        /// </summary>
        public IEventParser EventDefinition { get; set; }

        private StreamReader streamreader;
        private int currentLine;

        /// <summary>
        /// Constructor used to initialize a <code>SimpleLogReader</code> object.
        /// </summary>
        /// <param name="logfile">This <c>string</c> will be interpreted as URI and used to initialize a <see cref="StreamReader"/>.</param>
        public SimpleLogReader(string logfile)
        {
            this.currentLine = 0;
            this.Logfile = logfile;
        }

        /// <summary>
        /// Constructor used to initialize a <code>SimpleLogReader</code> objects with an already initialized <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="logfile">This <c>string</c> serves as the logfile's identifier.</param>
        /// <param name="streamreader"><see cref="StreamReader"/> object used to parse the logfile.</param>
        public SimpleLogReader(string logfile, StreamReader streamreader) : this(logfile)
        {
            this.currentLine = 0;
            this.streamreader = streamreader;
        }

        /// <summary>
        /// Iterates over the <c>Logfile</c> and returns each line that is a log event in respect to the specified definitions in <cref name="EventDefinition"/>.
        /// </summary>
        /// <returns>An iterator for log events.</returns>
        public IEnumerable<LogEvent> GetEventIterator()
        {
            if (this.streamreader == null)
                this.InitializeFileSteam();

            using (this.streamreader)
            {
                string line;
                while ((line = this.streamreader.ReadLine()) != null)
                {
                    this.currentLine++;
                    var element = new LogElement(line, this.currentLine);
                    List<LogEvent> events;

                    if (this.EventDefinition.GetEvent(element, out events))
                    {
                        foreach (var logEvent in events){
                            yield return logEvent;
                        }
                    }
                }
            }
            this.CloseFileStream();
        }

        private void InitializeFileSteam()
        {
            this.CheckConfigurationIsValid();

            if (!FilesystemIoHelper.FileExists(this.Logfile))
            {
                var message = "The specified logfile " + this.Logfile + " was not found";
                throw new FileNotFoundException(message, this.Logfile);
            }
            this.streamreader = FilesystemIoHelper.GetStreamReader(this.Logfile);
        }

        private void CloseFileStream()
        {
            this.streamreader.Dispose();
        }

        private void CheckConfigurationIsValid()
        {
            if (this.Logfile == null)
            {
                throw new BadConfigurationException("Logfile", "null");
            }
            if (this.EventDefinition == null)
            {
                throw new BadConfigurationException("EventDefinition", "null");
            }
        }
    }
}
