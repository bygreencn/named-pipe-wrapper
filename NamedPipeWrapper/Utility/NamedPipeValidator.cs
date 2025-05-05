using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NamedPipeWrapper.Utility
{
#pragma warning disable CS1591 
    public class NamedPipeValidator

    {
        // Regular rule:
        // start and end with letters/numbers,
        // allow symbols in the middle,
        // disable continuous symbols
        private static readonly Regex NameRegex = new Regex(
            @"^(?!.*[-_.]{2})[a-zA-Z0-9](?:[a-zA-Z0-9-_.]*[a-zA-Z0-9])?$",
            RegexOptions.Compiled);

        private static readonly Regex PathRegex = new Regex(
            @"^\\\\\.\\pipe\\(?<name>(?!.*[-_.]{2})[a-zA-Z0-9](?:[a-zA-Z0-9-_.]*[a-zA-Z0-9])?)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Extract valid names from input strings
        /// </summary>
        public static List<string> ExtractValidNames(string input)
        {
            var matches = NameRegex.Matches(input);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        public static List<string> ExtractNamesFromPath(string input)
        {
            var match = PathRegex.Match(input);
            return match.Success ?
                new List<string> { match.Groups["name"].Value } :
                new List<string>();
        }
    }
#pragma warning restore CS1591 
}