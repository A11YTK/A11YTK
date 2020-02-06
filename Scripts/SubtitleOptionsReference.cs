using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

namespace A11YTK
{

    [Serializable]
    public struct SubtitleOptions
    {

        public bool enabled;

        public Subtitle.Position defaultPosition;

        public float fontSize;

        public string fontForegroundColor;

        public string fontBackgroundColor;

        public bool showBackgroundColor;

    }

    [CreateAssetMenu(fileName = "SubtitleOptions", menuName = "A11YTK/Subtitle Options")]
    public class SubtitleOptionsReference : ScriptableObject
    {

        public bool enabled;

        public Subtitle.Position defaultPosition = Subtitle.Position.BOTTOM;

        public float fontSize = 30;

        public Color fontForegroundColor = Color.white;

        public Color fontBackgroundColor = Color.black;

        public bool showBackgroundColor = true;

        public TMP_FontAsset fontAsset;

        public Material fontMaterial;

        public void Save(string fileName, string directory)
        {

            var path = Path.Combine(directory, fileName);

            var binaryFormatter = new BinaryFormatter();

            using (var fs = File.Create(path))
            {

                try
                {

                    binaryFormatter.Serialize(fs,
                        new SubtitleOptions
                        {
                            enabled = enabled,
                            defaultPosition = defaultPosition,
                            fontSize = fontSize,
                            fontForegroundColor =
                                $"{fontForegroundColor.r},{fontForegroundColor.g},{fontForegroundColor.b},{fontForegroundColor.a}",
                            fontBackgroundColor =
                                $"{fontBackgroundColor.r},{fontBackgroundColor.g},{fontBackgroundColor.b},{fontBackgroundColor.a}",
                            showBackgroundColor = showBackgroundColor
                        });

                }
                catch (Exception err)
                {

                    Debug.LogError(err.Message);

                    throw;

                }
                finally
                {

                    fs.Close();

                }

            }

        }

        public void Save(string fileName)
        {

            Save(fileName, Application.persistentDataPath);

        }

        public void Load(string fileName, string directory)
        {

            var path = Path.Combine(directory, fileName);

            var binaryFormatter = new BinaryFormatter();

            using (var fs = File.OpenRead(path))
            {

                try
                {

                    var subtitleOptions = (SubtitleOptions)binaryFormatter.Deserialize(fs);

                    enabled = subtitleOptions.enabled;
                    defaultPosition = subtitleOptions.defaultPosition;
                    fontSize = subtitleOptions.fontSize;
                    fontForegroundColor = subtitleOptions.fontForegroundColor.ToColor();
                    fontBackgroundColor = subtitleOptions.fontBackgroundColor.ToColor();
                    showBackgroundColor = subtitleOptions.showBackgroundColor;

                }
                catch (Exception err)
                {

                    Debug.LogError(err.Message);

                    throw;

                }
                finally
                {

                    fs.Close();

                }

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
