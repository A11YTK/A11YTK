using System.Collections.Generic;
using UnityEngine;

namespace A11YTK
{

    public abstract class SubtitleSourceController : SubtitleController
    {

#pragma warning disable CS0649
        [SerializeField]
        [TextArea(1, 10)]
        protected string _subtitleText = "1\n0:0:1,0 --> 0:0:2,0\nHello, world.\n";

        public string subtitleText
        {
            get => _subtitleText;
            set => _subtitleText = value;
        }

        [SerializeField]
        protected TextAsset _subtitleTextAsset;

        public TextAsset subtitleTextAsset
        {
            get => _subtitleTextAsset;
            set => _subtitleTextAsset = value;
        }
#pragma warning restore CS0649

        protected List<SRT.Subtitle> _subtitles;

        protected SRT.Subtitle? _currentSubtitle;

        protected abstract double _elapsedTime { get; }

        protected abstract bool _isPlaying { get; }

        protected override void Awake()
        {

            if (_subtitleTextAsset != null)
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleTextAsset.text);

            }
            else
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleText);

            }

            base.Awake();

        }

        protected void FixedUpdate()
        {

            if (_subtitleOptions != null && _subtitleOptions.enabled && _isPlaying)
            {

                Tick();

            }

        }

        protected void Tick()
        {

            if (_currentSubtitle.HasValue &&
                _subtitleRenderer.isVisible &&
                (_elapsedTime < _currentSubtitle.Value.startTime ||
                 _elapsedTime > _currentSubtitle.Value.endTime))
            {

                _subtitleRenderer.Hide();

                _currentSubtitle = null;

            }

            if (!_currentSubtitle.HasValue)
            {

                for (var i = 0; i < _subtitles.Count; i += 1)
                {

                    if (_elapsedTime < _subtitles[i].endTime)
                    {

                        _currentSubtitle = _subtitles[i];

                        break;

                    }

                }

            }

            if (_currentSubtitle.HasValue &&
                !_subtitleRenderer.isVisible &&
                _elapsedTime >= _currentSubtitle.Value.startTime &&
                _elapsedTime <= _currentSubtitle.Value.endTime)
            {

                _subtitleRenderer.SetText(_currentSubtitle.Value.text);

                _subtitleRenderer.Show();

            }

        }

    }

}
