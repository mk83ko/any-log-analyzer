using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerCore
{
    public class StringHelper
    {
        public static bool TryGetMatch(string input, string regex, out List<string> matches)
        {
            matches = new List<String>();
            MatchCollection collectionOfMatches = (new Regex(regex)).Matches(input);
            foreach (Match match in collectionOfMatches.Cast<Match>())
            {
                matches.Add(match.Value);
            }
            return (collectionOfMatches.Count > 0);
        }
    }
}
