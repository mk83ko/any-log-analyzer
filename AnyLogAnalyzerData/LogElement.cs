namespace Mkko
{
    /// <summary>
    /// An instance of this class represents a single element of a parsed logfile. This object is used to be checked for <see cref="LogEvent"/>s defined in a <see cref="DefinitionElement"/>.
    /// </summary>
    public class LogElement
    {
        /// <summary>
        /// This property holds a <c>string</c> representation of a single element of a parsed logfile.
        /// </summary>
        public string LogMessage { get; set; }
        /// <summary>
        /// This property holds an <c>int</c> denominating the order of a <see cref="LogElement"/> in respect to other <see cref="LogElement"/>s.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Constructor to be used to initialize an object of type <see cref="LogElement"/>.
        /// </summary>
        /// <param name="line">The <c>string</c> representation of a single element in a logfile.</param>
        /// <param name="lineNumber">An <c>int</c> value representing the order of this <c>LogElement</c> in respect to other ones.</param>
        public LogElement(string line, int lineNumber)
        {
            LogMessage = line;
            LineNumber = lineNumber;
        }
    }
}
