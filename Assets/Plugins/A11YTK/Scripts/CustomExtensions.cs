using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace A11YTK
{

    public static class CustomExtensions
    {

        public static List<List<string>> ChunkListWithPatternDelimiter(this List<string> lines, string pattern)
        {

            var matches = new List<List<string>>();

            var currentIndex = 0;

            for (var i = 0; i < lines.Count; i += 1)
            {

                if (!Regex.IsMatch(lines[i], pattern))
                {
                    continue;
                }

                matches.Add(lines.GetRange(currentIndex, i - currentIndex));

                currentIndex = i + 1;

            }

            if (currentIndex < lines.Count)
            {

                matches.Add(lines.GetRange(currentIndex, lines.Count - currentIndex));

            }

            return matches;

        }

    }

}
