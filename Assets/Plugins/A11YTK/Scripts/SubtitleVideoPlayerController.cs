using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Video Player Controller")]
    public class SubtitleVideoPlayerController : SubtitleSourceController
    {

#pragma warning disable CS0649
        [FormerlySerializedAs("_videoSource")]
        [SerializeField]
        private VideoPlayer _videoPlayer;
#pragma warning restore CS0649

        protected override double _elapsedTime => _videoPlayer.time;

        protected override bool _isPlaying =>
            _videoPlayer && _videoPlayer.isPlaying && _videoPlayer.time < _videoPlayer.length;

#if UNITY_EDITOR
        protected override void OnValidate()
        {

            base.OnValidate();

            if (_videoPlayer == null)
            {

                _videoPlayer = gameObject.GetComponent<VideoPlayer>();

            }

        }
#endif

    }

}
