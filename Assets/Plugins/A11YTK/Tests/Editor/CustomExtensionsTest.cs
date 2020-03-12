using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace A11YTK.Tests
{

    public class CustomExtensionsTest
    {

        [Test]
        public void ChunkListWithPatternDelimiterFromList()
        {

            var list = new List<string>(new[] { "1", "hello", "", "2", "world", "!", "", "3", "wave", "" });

            var chunks = list.ChunkListWithPatternDelimiter(@"^\s*$");

            Assert.AreEqual(3, chunks.Count);

            Assert.AreEqual(2, chunks[0].Count);
            Assert.AreEqual("1", chunks[0][0]);
            Assert.AreEqual("hello", chunks[0][1]);

            Assert.AreEqual(3, chunks[1].Count);
            Assert.AreEqual("2", chunks[1][0]);
            Assert.AreEqual("world", chunks[1][1]);
            Assert.AreEqual("!", chunks[1][2]);

            Assert.AreEqual(2, chunks[2].Count);
            Assert.AreEqual("3", chunks[2][0]);
            Assert.AreEqual("wave", chunks[2][1]);

        }

        [Test]
        public void ToColorFromString()
        {

            var color = "rgba(255, 0, 0, 100)".ToColor();

            Assert.AreEqual(255, color.r);
            Assert.AreEqual(0, color.g);
            Assert.AreEqual(0, color.b);
            Assert.AreEqual(100, color.a);

        }

        [Test]
        public void ResetRectTransform()
        {

            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);

            var gameObject = new GameObject("Canvas");
            var canvas = gameObject.AddComponent<Canvas>();

            var rectTransform = canvas.GetComponent<RectTransform>();

            rectTransform.ResetRectTransform();

            Assert.AreEqual(Vector2.zero, rectTransform.anchorMin);
            Assert.AreEqual(Vector2.one, rectTransform.anchorMax);

            Assert.AreEqual(Vector2.zero, rectTransform.offsetMin);
            Assert.AreEqual(Vector2.zero, rectTransform.offsetMax);

        }

    }

}
