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
        public static string ToLiteral(string original)
        {
            string literal = "";
            //TODO implement something like http://stackoverflow.com/questions/323640/can-i-convert-a-c-sharp-string-value-to-an-escaped-string-literal
            return literal;
        }

        public static bool ReturnSimpleMatch(string element, string pattern, out string match)
        {
            match = "";
            return false;
        }

        public static bool ReturnRegexMatch(string element, string pattern, out string match)
        {
            match = "";
            return false;
        }

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
