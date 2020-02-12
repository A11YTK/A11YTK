using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A11YTK
{

    public abstract class SubtitleController : MonoBehaviour
    {

        protected const float DEFAULT_VOLUME_SCALE = 1f;

#pragma warning disable CS0649
        [SerializeField]
        [TextArea(1, 10)]
        protected string _subtitleText = "1\n0:0:1,0 --> 0:0:2,0\nHello, world.\n";

        [SerializeField]
        protected TextAsset _subtitleTextAsset;

        [SerializeField]
        protected Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        protected Subtitle.Type _type;

        [SerializeField]
        protected SubtitleOptionsReference _subtitleOptions;

        [SerializeField]
        protected bool _autoPlaySubtitles = true;
#pragma warning restore CS0649

        public Subtitle.Position position =>
            _position.Equals(Subtitle.Position.AUTO) ? _subtitleOptions.defaultPosition : _position;

        public SubtitleOptionsReference subtitleOptions => _subtitleOptions;

        protected SubtitleRenderer _subtitleRenderer;

        protected List<SRT.Subtitle> _subtitles;

        protected Coroutine _loopThroughSubtitleLinesCoroutine;

        protected abstract double _elapsedTime { get; }

        protected abstract bool _isPlaying { get; }

        protected void Awake()
        {

            _subtitleRenderer = gameObject.GetComponent<SubtitleRenderer>();

            if (_subtitleTextAsset != null)
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleTextAsset.text);

            }
            else
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleText);

            }

            if (subtitleOptions == null)
            {

                Debug.LogWarning("Subtitle options asset is missing!");

            }

        }

        private IEnumerator Start()
        {

            while (_autoPlaySubtitles)
            {

                if (_isPlaying && _loopThroughSubtitleLinesCoroutine == null)
                {

                    Play();

                }

                yield return null;

            }

        }

        public virtual void Play()
        {

            if (_loopThroughSubtitleLinesCoroutine != null)
            {
                return;
            }

            _loopThroughSubtitleLinesCoroutine = StartCoroutine(LoopThroughSubtitleLines());

        }

        public virtual void Stop()
        {

            if (_loopThroughSubtitleLinesCoroutine == null)
            {
                return;
            }

            StopCoroutine(_loopThroughSubtitleLinesCoroutine);

            _loopThroughSubtitleLinesCoroutine = null;

        }

        protected IEnumerator LoopThroughSubtitleLines()
        {

            var currentSubtitleIndex = 0;

            while (currentSubtitleIndex < _subtitles.Count)
            {

                if (_subtitleRenderer.isVisible &&
                    _elapsedTime >= _subtitles[currentSubtitleIndex].endTime / 1000)
                {

                    _subtitleRenderer.Hide();

                    currentSubtitleIndex += 1;

                }
                else if (!_subtitleRenderer.isVisible &&
                         _elapsedTime >= _subtitles[currentSubtitleIndex].startTime / 1000)
                {

                    _subtitleRenderer.Show();

                    _subtitleRenderer.SetText(_subtitles[currentSubtitleIndex].text);

                }

                yield return null;

            }

            _subtitleRenderer.Hide();

            _loopThroughSubtitleLinesCoroutine = null;

        }

    }

}
