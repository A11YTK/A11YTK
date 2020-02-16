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

            var words = Regex.Split(text, @"(?:\s+)");

            foreach (var word in words)
            {

                var combinedWords = $"{lines[lines.Count - 1]} {word}".Trim();

                var valueSizeDelta = textMesh.GetPreferredValues(combinedWords);

                if (valueSizeDelta.x > wrapWidth)
                {

                    lines.Add(word);

                }
                else
                {

                    lines[lines.Count - 1] = combinedWords;

                }

            }

            return string.Join("\n", lines).Trim();

        }

        public static void ResetRectTransform(this RectTransform rectTransform)
        {

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

        }

        public static void ResizeRectTransformToMatchCamera(this RectTransform rectTransform, Camera camera)
        {

            var distance = Vector3.Distance(camera.transform.position, rectTransform.gameObject.transform.position);

            var camHeight = 2 * distance * Mathf.Tan(Mathf.Deg2Rad * (camera.fieldOfView / 2));

            rectTransform.sizeDelta = new Vector2(camHeight * camera.aspect, camHeight);

        }

    }

}
