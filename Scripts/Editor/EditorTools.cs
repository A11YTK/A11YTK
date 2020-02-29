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
                .FirstOrDefault();

        [MenuItem("Window/A11YTK/Setup Audio Sources in Scene")]
        public static void SetupAudioSources()
        {

            Undo.SetCurrentGroupName("setup audio source subtitles");

            var group = Undo.GetCurrentGroup();

            var audioSources = Object.FindObjectsOfType<AudioSource>();

            foreach (var audioSource in audioSources)
            {

                var subtitleTextAsset = FindAssetWithNameInDirectory<TextAsset>(
                    $"{Path.GetFileName(audioSource.clip.name)}.srt",
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(audioSource.clip)));

                if (!subtitleTextAsset)
                {

                    Debug.LogWarning($"There is no subtitle file for {AssetDatabase.GetAssetPath(audioSource.clip)}");

                    continue;

                }

                if (!audioSource.gameObject.TryGetComponent(out SubtitleController subtitleController))
                {

                    subtitleController = Undo.AddComponent<SubtitleVideoPlayerController>(audioSource.gameObject);

                }

                if (subtitleController.subtitleTextAsset == null)
                {

                    Undo.RecordObject(subtitleController, "set subtitle text asset");

                    subtitleController.subtitleTextAsset = subtitleTextAsset;

                }

                Undo.CollapseUndoOperations(group);

            }

        }

        [MenuItem("Window/A11YTK/Setup Video Players in Scene")]
        public static void SetupVideoSources()
        {

            Undo.SetCurrentGroupName("setup video player subtitles");

            var group = Undo.GetCurrentGroup();

            var videoPlayers = Object.FindObjectsOfType<VideoPlayer>();

            foreach (var videoPlayer in videoPlayers)
            {

                var subtitleTextAsset = FindAssetWithNameInDirectory<TextAsset>(
                    $"{Path.GetFileName(videoPlayer.clip.name)}.srt",
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(videoPlayer.clip)));

                if (!subtitleTextAsset)
                {

                    Debug.LogWarning($"There is no subtitle file for {AssetDatabase.GetAssetPath(videoPlayer.clip)}");

                    continue;

                }

                if (!videoPlayer.gameObject.TryGetComponent(out SubtitleController subtitleController))
                {

                    subtitleController = Undo.AddComponent<SubtitleVideoPlayerController>(videoPlayer.gameObject);

                }

                if (subtitleController.subtitleTextAsset == null)
                {

                    Undo.RecordObject(subtitleController, "set subtitle text asset");

                    subtitleController.subtitleTextAsset = subtitleTextAsset;

                }

                Undo.CollapseUndoOperations(group);

            }

        }

    }

}
#endif
