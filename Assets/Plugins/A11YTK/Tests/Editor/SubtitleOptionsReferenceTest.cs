using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace A11YTK.Tests
{

    public static class SubtitleOptionsReferenceTest
    {

        private const float COLOR_EPSILON = 0.01f;

        private const string subtitlesOptionsFileName = "SubtitleOptions.dat";

        private static string subtitlesOptionsPath =>
            Path.Combine(Application.persistentDataPath, subtitlesOptionsFileName);

        [SetUp]
        [TearDown]
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
            subtitleOptions.fontColor = Color.yellow;

            subtitleOptions.Save(subtitlesOptionsFileName);

            var loadedSubtitleOptions = ScriptableObject.CreateInstance<SubtitleOptionsReference>();

            loadedSubtitleOptions.Load(subtitlesOptionsFileName);

            Assert.AreEqual(true, loadedSubtitleOptions.enabled);
            Assert.AreEqual(24, loadedSubtitleOptions.fontSize);

            Assert.Less(Mathf.Abs(Color.yellow.r - loadedSubtitleOptions.fontColor.r), COLOR_EPSILON);
            Assert.Less(Mathf.Abs(Color.yellow.g - loadedSubtitleOptions.fontColor.g), COLOR_EPSILON);
            Assert.Less(Mathf.Abs(Color.yellow.b - loadedSubtitleOptions.fontColor.b), COLOR_EPSILON);
            Assert.Less(Mathf.Abs(Color.yellow.a - loadedSubtitleOptions.fontColor.a), COLOR_EPSILON);

        }

    }

}
