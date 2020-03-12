using System.Collections.Generic;
using NUnit.Framework;

namespace A11YTK.Tests
{

    public class CustomExtensionsTest
    {

        [Test]
        public void ChunkListWithPatternDelimiterFromList()
        {

            var list = new List<string>(new[] { "1", "hello", "", "2", "world", "" });

            var chunks = list.ChunkListWithPatternDelimiter(@"^\s*$");

            Assert.AreEqual(2, chunks.Count);
            Assert.AreEqual(2, chunks[0].Count);
            Assert.AreEqual("1", chunks[0][0]);
            Assert.AreEqual("hello", chunks[0][1]);
            Assert.AreEqual("2", chunks[1][0]);
            Assert.AreEqual("world", chunks[1][1]);

        }

    }

}
