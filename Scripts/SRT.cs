using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace A11YTK
{

    public static class SRT
    {

        public struct Subtitle
        {

            public int id;

            public double startTime;

            public double endTime;

            public string text;

        }

        public static readonly string[] TIMESTAMP_DELIMITER = { "-->" };

        public static double ParseMillisecondsFromTimeStamp(string timestamp, out TimeSpan results)
        {

            TimeSpan.TryParse(timestamp.Trim().Replace(',', '.'), out results);

            return results.TotalMilliseconds;

        }

        public static void ParseTimeFromContent(string content, out double startTime, out double endTime)
        {

            var times = content.Split(TIMESTAMP_DELIMITER, StringSplitOptions.None);

            try
            {

                startTime = ParseMillisecondsFromTimeStamp(times[0], out _);
                endTime = ParseMillisecondsFromTimeStamp(times[1], out _);

            }
            catch (Exception _)
            {

                startTime = -1;
                endTime = -1;

                throw new WarningException("Non-valid timestamp range.");

            }

        }

        public static List<Subtitle> ParseSubtitlesFromFile(string path)
        {

            return ParseSubtitlesFromString(File.ReadAllText(path, Encoding.UTF8));

        }

        public static List<Subtitle> ParseSubtitlesFromString(string content)
        {

            var subtitles = new List<Subtitle>();

            var matches = content.Split(new char[] { '\n' }).ToList().ChunkListWithPatternDelimiter(@"^\s*$");

            foreach (var match in matches)
            {

                var subtitle = new Subtitle();

                int.TryParse(match[0], out subtitle.id);

                ParseTimeFromContent(match[1], out subtitle.startTime, out subtitle.endTime);

                var text = new StringBuilder();

                for (var i = 2; i < match.Count; i += 1)
                {

                    text.Append(match[i]);
                    text.Append(Environment.NewLine);

                }

                subtitle.text = text.ToString().Trim();

                subtitles.Add(subtitle);

            }

            return subtitles;

        }

    }

}
