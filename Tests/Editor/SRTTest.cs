using System;
using NUnit.Framework;

namespace A11YTK.Tests
{

    public class SRTTest
    {

        private const string MOCK_SUBTITLE_CONTENTS = @"1
00:00:04,200 --> 00:00:08,200
Hello, world.

2
00:00:12,200 --> 00:00:20,200
Hello, world!


3
00:00:24,200 --> 00:00:28,200
I can see you. Can you see me?



4
00:00:32,200 --> 00:00:36,200
If you can see me,
can you wave?

5
00:00:40,200 --> 00:00:44,200
Fine. If you can't wave, just yell out.



";

        [Test]
        public void ParseSubtitlesFromSRTContents()
        {

            var subtitles =
                SRT.ParseSubtitlesFromString(MOCK_SUBTITLE_CONTENTS);

            Assert.AreEqual(1, subtitles[0].id);
            Assert.AreEqual(4200d, subtitles[0].startTime);
            Assert.AreEqual(8200d, subtitles[0].endTime);
            Assert.AreEqual("Hello, world.", subtitles[0].text);

            Assert.AreEqual(4, subtitles[3].id);
            Assert.AreEqual(32200d, subtitles[3].startTime);
            Assert.AreEqual(36200d, subtitles[3].endTime);
            Assert.AreEqual("If you can see me,\ncan you wave?", subtitles[3].text);

            Assert.AreEqual(5, subtitles[4].id);
            Assert.AreEqual(40200d, subtitles[4].startTime);
            Assert.AreEqual(44200d, subtitles[4].endTime);
            Assert.AreEqual("Fine. If you can't wave, just yell out.", subtitles[4].text);

        }

        [Test]
        public void ParseMillisecondsFromTimeStampString()
        {

            Assert.AreEqual(4200d, SRT.ParseMillisecondsFromTimeStamp("0:0:4,200", out var _));

        }

        [Test]
        public void ParseTimeFromContentString()
        {

            SRT.ParseTimeFromContent("0:0:4,200 --> 0:0:8,200", out var startTime, out var endTime);

            Assert.AreEqual(4200d, startTime);
            Assert.AreEqual(8200d, endTime);

        }

        [Test]
        public void ParseTimeFromContentStringWithException()
        {

            Assert.Throws<InvalidOperationException>(
                () =>
                    SRT.ParseTimeFromContent("0:0:4,200", out var startTime, out var endTime)
            );

        }

    }

}
