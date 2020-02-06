using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace A11YTK.Tests
{

    public static class SubtitleOptionsReferenceTest
    {

        private const string subtitlesOptionsFileName = "SubtitleOptions.dat";

        private static string subtitlesOptionsPath =>
            Path.Combine(Application.persistentDataPath, subtitlesOptionsFileName);

        [SetUp, TearDown]
        public static void DeleteFiles()
        {

            FileUtil.DeleteFileOrDirectory(subtitlesOptionsPath);

        }

        [Test]
        public static void SaveAndLoad()
        {

            var subtitleOptions = ScriptableObject.CreateInstance<SubtitleOptionsReference>();

            subtitleOptions.enabled = true;
            subtitleOptions.fontSize = 24;
            subtitleOptions.fontForegroundColor = Color.yellow;

            subtitleOptions.Save(subtitlesOptionsFileName);

            var loadedSubtitleOptions = ScriptableObject.CreateInstance<SubtitleOptionsReference>();

            loadedSubtitleOptions.Load(subtitlesOptionsFileName);

            Assert.AreEqual(true, loadedSubtitleOptions.enabled);
            Assert.AreEqual(24, loadedSubtitleOptions.fontSize);

            Assert.IsTrue(Mathf.Approximately(Color.yellow.r, loadedSubtitleOptions.fontForegroundColor.r));
            Assert.IsTrue(Mathf.Approximately(Color.yellow.g, loadedSubtitleOptions.fontForegroundColor.g));
            Assert.IsTrue(Mathf.Approximately(Color.yellow.b, loadedSubtitleOptions.fontForegroundColor.b));
            Assert.IsTrue(Mathf.Approximately(Color.yellow.a, loadedSubtitleOptions.fontForegroundColor.a));

        }

    }

}
