using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle VideoPlayer Controller")]
    [RequireComponent(typeof(SubtitleRenderer))]
    public class SubtitleVideoPlayerController : SubtitleController
    {

#pragma warning disable CS0649
        [SerializeField]
        private VideoPlayer _videoSource;
#pragma warning restore CS0649

        protected override double elapsedTime => _videoSource.time;

        private IEnumerator Start()
        {

            while (_autoPlaySubtitles)
            {

                if (_videoSource.isPlaying && _loopThroughSubtitleLinesCoroutine == null)
                {

                    Play();

                }

                yield return null;

            }

        }

        public override void Play()
        {

            base.Play();

            if (_videoSource)
            {

                _videoSource.Play();

            }

        }

        public override void Stop()
        {

            base.Stop();

            if (_videoSource)
            {

                _videoSource.Stop();

            }

        }

        private void OnValidate()
        {

            if (_videoSource == null)
            {

                _videoSource = gameObject.GetComponent<VideoPlayer>();

            }

        }

    }

}