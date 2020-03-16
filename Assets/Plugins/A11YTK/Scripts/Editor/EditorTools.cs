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

        [MenuItem("Window/A11YTK/Setup Audio Sources in Scene", true)]
        public static bool ValidateSetupAudioSources()
        {

            return !EditorApplication.isPlaying;

        }

        [MenuItem("Window/A11YTK/Setup Audio Sources in Scene")]
        public static void SetupAudioSources()
        {

            Undo.SetCurrentGroupName("setup audio source subtitles");

            var group = Undo.GetCurrentGroup();

            var audioSources = Object.FindObjectsOfType<AudioSource>();

            foreach (var source in audioSources)
            {

                var clip = source.clip;

                var subtitleTextAsset = FindAssetWithNameInDirectory<TextAsset>(
                    $"{clip.name}.srt",
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(clip)));

                if (!subtitleTextAsset)
                {

                    Debug.LogWarning($"There is no subtitle file for {AssetDatabase.GetAssetPath(source.clip)}");

                    continue;

                }

                if (!source.gameObject.TryGetComponent(out SubtitleSourceController subtitleController))
                {

                    subtitleController = Undo.AddComponent<SubtitleVideoPlayerController>(source.gameObject);

                }

                if (subtitleController.subtitleTextAsset == null)
                {

                    Undo.RecordObject(subtitleController, "set subtitle text asset");

                    subtitleController.subtitleTextAsset = subtitleTextAsset;

                }

                Undo.CollapseUndoOperations(group);

            }

        }

        [MenuItem("Window/A11YTK/Setup Video Players in Scene", true)]
        public static bool ValidateSetupVideoSources()
        {

            return !EditorApplication.isPlaying;

        }

        [MenuItem("Window/A11YTK/Setup Video Players in Scene")]
        public static void SetupVideoSources()
        {

            Undo.SetCurrentGroupName("setup video player subtitles");

            var group = Undo.GetCurrentGroup();

            var videoPlayers = Object.FindObjectsOfType<VideoPlayer>();

            foreach (var source in videoPlayers)
            {

                var clip = source.clip;

                var subtitleTextAsset = FindAssetWithNameInDirectory<TextAsset>(
                    $"{clip.name}.srt",
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(clip)));

                if (!subtitleTextAsset)
                {

                    Debug.LogWarning($"There is no subtitle file for {AssetDatabase.GetAssetPath(source.clip)}");

                    continue;

                }

                if (!source.gameObject.TryGetComponent(out SubtitleSourceController subtitleController))
                {

                    subtitleController = Undo.AddComponent<SubtitleVideoPlayerController>(source.gameObject);

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
