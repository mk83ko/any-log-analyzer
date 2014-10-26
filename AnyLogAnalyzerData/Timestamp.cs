namespace Mkko
{
    /// <summary>
    /// This class maintains a <see cref="Pattern"/> and <see cref="Format"/> used to detect timestamp information in a logfile.
    /// </summary>
    public class Timestamp
    {
// ReSharper disable CSharpWarnings::CS1591
        public string Pattern { get; set; }
        public string Format { get; set; }
// ReSharper restore CSharpWarnings::CS1591
    }
}
