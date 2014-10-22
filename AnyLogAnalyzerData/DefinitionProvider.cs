using System.Collections.Generic;

namespace Mkko
{
    /// <summary>
    /// Wrapper class to connect a <see cref="DefinitionElement"/> with a <see cref="Timestamp"/>.
    /// </summary>
    public class DefinitionProvider
    {
        // ReSharper disable once CSharpWarnings::CS1591
        public Timestamp Timestamp { get; set; }
        /// <summary>
        /// Property holding a <see cref="List{T}"/> of <see cref="DefinitionElement"/>s.
        /// </summary>
        public List<DefinitionElement> Definitions { get; set; }
    }
}
