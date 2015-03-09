using System;

namespace Mkko
{
    /// <summary>
    /// This class serves as exception to be thrown if any configuration parameter is missing or incorrect.
    /// </summary>
    public class BadConfigurationException : Exception
    {
        /// <summary>
        /// Constructor to create an exception with a custom message.
        /// </summary>
        /// <param name="message">Exception's message will be set to provided <c>string</c>.</param>
        public BadConfigurationException(string message) : base(message)
        {

        }

        /// <summary>
        /// Constructor to create an exception with parameter name and value of parameter that is somehow incorrect.
        /// </summary>
        /// <param name="parameter">Id of the incorrect or missing parameter as <c>string</c>.</param>
        /// <param name="value">Value of the incorrect or missing parameter as <c>string</c>.</param>
        public BadConfigurationException(string parameter, string value)
            : base("error in configuration: parameter " + parameter + " is not valid. (value: " + value +").")
        {

        }
    }
}
