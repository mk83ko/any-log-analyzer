using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mkko
{
    /// <summary>
    /// Class to collect frequently used operations on <c>string</c>s.
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Given an input <c>string</c> as well as an <c>string</c> representing a regular expression, this method will try to retrieve matches from the input <c>string</c> and
        /// stores them in a <see cref="List{T}"/> of <c>string</c>s.
        /// </summary>
        /// <param name="input">input <c>string</c> that should be parsed.</param>
        /// <param name="regex">a <c>string</c> representing a regular expression.</param>
        /// <param name="matches">a <see cref="List{T}"/> of <c>string</c> for storing the matches found in <paramref name="input"/>
        ///     <remarks>this is an out parameter.</remarks>
        /// </param>
        /// <returns><c>true</c> if any part of <paramref name="input"/> matches <paramref name="regex"/> or <c>false</c> else.</returns>
        public static bool TryGetMatch(string input, string regex, out List<string> matches)
        {
            matches = new List<String>();
            if (string.IsNullOrEmpty(regex) || string.IsNullOrEmpty(input))
                return false;

            var collectionOfMatches = (new Regex(regex)).Matches(input);
            foreach (var match in collectionOfMatches.Cast<Match>())
            {
                matches.Add(match.Value);
            }
            return (collectionOfMatches.Count > 0);
        }
    }
}
