using UnityEngine;
using UnityEngine.Video;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Video Player Controller")]
    public class SubtitleVideoPlayerController : SubtitleSourceController
    {

#pragma warning disable CS0649
        [SerializeField]
        private VideoPlayer _videoSource;
#pragma warning restore CS0649

        protected override double _elapsedTime => _videoSource.time;

        protected override bool _isPlaying =>
            _videoSource && _videoSource.isPlaying && _videoSource.time < _videoSource.length;

#if UNITY_EDITOR
        protected override void OnValidate()
        {

            base.OnValidate();

            if (_videoSource == null)
            {

                _videoSource = gameObject.GetComponent<VideoPlayer>();

            }

        }
#endif

    }

}
