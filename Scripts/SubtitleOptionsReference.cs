using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

namespace A11YTK
{

    [Serializable]
    public struct SubtitleOptions
    {

        public bool enabled;

        public float fontSize;

        public string fontColor;

        public string backgroundColor;

        public bool showBackground;

    }

    [CreateAssetMenu(fileName = "SubtitleOptions", menuName = "A11YTK/Subtitle Options")]
    public class SubtitleOptionsReference : ScriptableObject
    {

        public bool enabled;

        public Subtitle.Position defaultPosition = Subtitle.Position.BOTTOM;
        
        public Subtitle.Type defaultType = Subtitle.Type.HEADSET;

        public float screenPadding = 10;

        [Header("Text")]
        public float fontSize = 30;

        public Color fontColor = Color.white;

        public TMP_FontAsset fontAsset;

        public Material fontMaterial;

        public TextAlignmentOptions textAlignment = TextAlignmentOptions.Midline;

        [Header("Background")]
        public bool showBackground = true;

        public Color backgroundColor = Color.black;

        public Sprite backgroundSprite;

        public float backgroundPadding = 10;

        public void Save(string fileName, string directory)
        {

            var path = Path.Combine(directory, fileName);

            try
            {

                File.WriteAllText(path, JsonUtility.ToJson(
                    new SubtitleOptions
                    {
                        enabled = enabled,
                        fontSize = fontSize,
                        fontColor = fontColor.ToString(),
                        backgroundColor = backgroundColor.ToString(),
                        showBackground = showBackground
                    }));

            }
            catch (Exception err)
            {

                Debug.LogError(err.Message);

                throw;

            }

        }

        public void Save(string fileName)
        {

            Save(fileName, Application.persistentDataPath);

        }

        public void Load(string fileName, string directory)
        {

            var path = Path.Combine(directory, fileName);

            try
            {

                var subtitleOptions = JsonUtility.FromJson<SubtitleOptions>(File.ReadAllText(path, Encoding.UTF8));

                enabled = subtitleOptions.enabled;
                fontSize = subtitleOptions.fontSize;
                fontColor = subtitleOptions.fontColor.ToColor();
                backgroundColor = subtitleOptions.backgroundColor.ToColor();
                showBackground = subtitleOptions.showBackground;

            }
            catch (Exception err)
            {

                Debug.LogError(err.Message);

                throw;

            }

        }

        public void Load(string fileName)
        {

            Load(fileName, Application.persistentDataPath);

        }

        public void Delete(string fileName, string directory)
        {

            var path = Path.Combine(directory, fileName);

            File.Delete(path);

        }

        public void Delete(string fileName)
        {

            Delete(fileName, Application.persistentDataPath);

        }

    }

}
