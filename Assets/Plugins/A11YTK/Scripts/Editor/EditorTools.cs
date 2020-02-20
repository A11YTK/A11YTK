#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace A11YTK.Editor
{

    public static class EditorTools
    {

        private static T FindAssetWithNameInDirectory<T>(string name, string directory) =>
            AssetDatabase
                .FindAssets(name, new[] { directory })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(T)))
                .OfType<T>()
                .First();

        [MenuItem("Window/A11YTK/Setup Audio Sources in Scene")]
        public static void SetupAudioSources()
        {

            var audioSources = Object.FindObjectsOfType<AudioSource>();

            foreach (var audioSource in audioSources)
            {

                if (!audioSource.gameObject.TryGetComponent(out SubtitleController subtitleController))
                {

                    subtitleController = audioSource.gameObject.AddComponent<SubtitleAudioSourceController>();

                }

                var subtitleTextAsset = FindAssetWithNameInDirectory<TextAsset>(
                    $"{Path.GetFileName(audioSource.clip.name)}.srt",
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(audioSource.clip)));

                if (subtitleController.subtitleTextAsset == null)
                {

                    subtitleController.subtitleTextAsset = subtitleTextAsset;

                }

            }

        }

        [MenuItem("Window/A11YTK/Setup Video Players in Scene")]
        public static void SetupVideoSources()
        {

            var videoPlayers = Object.FindObjectsOfType<VideoPlayer>();

            foreach (var videoPlayer in videoPlayers)
            {

                if (!videoPlayer.gameObject.TryGetComponent(out SubtitleController subtitleController))
                {

                    subtitleController = videoPlayer.gameObject.AddComponent<SubtitleVideoPlayerController>();

                }

                var subtitleTextAsset = FindAssetWithNameInDirectory<TextAsset>(
                    $"{Path.GetFileName(videoPlayer.clip.name)}.srt",
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(videoPlayer.clip)));

                if (subtitleController.subtitleTextAsset == null)
                {

                    subtitleController.subtitleTextAsset = subtitleTextAsset;

                }

            }

        }

    }

}
#endif
