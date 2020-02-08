using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

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

        public static Color ToColor(this string color)
        {

            var values = color.Replace("RGBA", "").Trim('(', ')').Split(',');

            float.TryParse(values[0], out var r);
            float.TryParse(values[1], out var g);
            float.TryParse(values[2], out var b);
            float.TryParse(values[3], out var a);

            return new Color(r, g, b, a);

        }

        public static string WrapText(this TextMeshProUGUI textMesh, string text, float wrapWidth)
        {

            var lines = new List<string> { "" };

            var words = Regex.Split(text, @"(\s+)");

            var currentLine = 0;
            var currentLineWidth = 0f;

            foreach (var word in words)
            {

                var valueSizeDelta = textMesh.GetPreferredValues(word);

                currentLineWidth += valueSizeDelta.x;

                if (currentLineWidth > wrapWidth)
                {

                    currentLine += 1;

                    currentLineWidth = valueSizeDelta.x;

                    lines.Add("");

                }

                lines[currentLine] += word;

            }

            return string.Join("\n", lines).Trim();

        }

    }

}
