using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Default constructor provides empty Timestamp and Definitions properties.
        /// </summary>
        public DefinitionProvider()
        {
            this.Timestamp = new Timestamp();
            this.Definitions = new List<DefinitionElement>();
        }

        /// <summary>
        /// returns a list of all defined metadata keys
        /// </summary>
        public List<string> GetAllMetadataKeys()
        {
            var keys = new List<string>();
            foreach (var element in this.Definitions)
                keys = keys.Union(element.GetMetadataKeys()).ToList();

            return keys;
        } 
    }
}
