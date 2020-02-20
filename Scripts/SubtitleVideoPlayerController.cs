#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Video;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Video Player Controller")]
    [RequireComponent(typeof(SubtitleRenderer))]
    public class SubtitleVideoPlayerController : SubtitleController
    {

#pragma warning disable CS0649
        [SerializeField]
        private VideoPlayer _videoSource;
#pragma warning restore CS0649

        protected override double _elapsedTime => _videoSource.time;

        protected override bool _isPlaying =>
            _videoSource && _videoSource.isPlaying && _videoSource.time < _videoSource.length;

#if UNITY_EDITOR
        protected void OnValidate()
        {

            if (_subtitleOptions == null)
            {

                _subtitleOptions =
                    AssetDatabase.LoadAssetAtPath<SubtitleOptionsReference>(AssetDatabase.GUIDToAssetPath(AssetDatabase
                        .FindAssets("t:SubtitleOptionsReference", null)
                        .FirstOrDefault()));

            }

            if (_videoSource == null)
            {

                _videoSource = gameObject.GetComponent<VideoPlayer>();

            }

        }
#endif

    }

}
