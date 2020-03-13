using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace A11YTK
{

    public abstract class SubtitleController : MonoBehaviour
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

        [SerializeField]
        protected Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        protected Subtitle.Mode _mode = Subtitle.Mode.AUTO;

        [SerializeField]
        protected SubtitleOptionsReference _subtitleOptions;
#pragma warning restore CS0649

        public Subtitle.Position position =>
            _position.Equals(Subtitle.Position.AUTO) ? _subtitleOptions.defaultPosition : _position;

        public Subtitle.Mode mode =>
            _mode.Equals(Subtitle.Mode.AUTO) ? _subtitleOptions.defaultMode : _mode;

        public SubtitleOptionsReference subtitleOptions => _subtitleOptions;

        protected SubtitleRenderer _subtitleRenderer;

        protected List<SRT.Subtitle> _subtitles;

        protected abstract double _elapsedTime { get; }

        protected abstract bool _isPlaying { get; }

        protected SRT.Subtitle? _currentSubtitle;

        protected void Awake()
        {

            if (_subtitleTextAsset != null)
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleTextAsset.text);

            }
            else
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleText);

            }

            if (_subtitleOptions == null)
            {

                Debug.LogError("Subtitle options asset is missing!");

            }

            if (subtitleOptions.defaultMode.Equals(Subtitle.Mode.SCREEN) && UnityEngine.XR.XRSettings.enabled)
            {

                Debug.LogWarning("Subtitles will not render in SCREEN mode while running in VR!");

                subtitleOptions.defaultMode = Subtitle.Mode.HEADSET;

            }

            var subtitlePrefab =
                Resources.LoadAll<GameObject>("Prefabs")
                    .First(material =>
                        material.name.StartsWith("Subtitle") && material.name.Contains(mode.ToString()));

            _subtitleRenderer = Instantiate(subtitlePrefab).GetComponent<SubtitleRenderer>();

            _subtitleRenderer.mode = mode;
            _subtitleRenderer.position = position;
            _subtitleRenderer.targetTransform = gameObject.transform;
            _subtitleRenderer.targetCollider = gameObject.GetComponent<Collider>();

            _subtitleRenderer.SetOptions(_subtitleOptions);

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

                _subtitleRenderer.Show();

                _subtitleRenderer.SetText(_currentSubtitle.Value.text);

            }

        }

        private void OnEnable()
        {

            _subtitleRenderer.Show();

        }

        private void OnDisable()
        {

            _subtitleRenderer.Hide();

        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {

            if (_subtitleOptions == null)
            {

                _subtitleOptions =
                    AssetDatabase.LoadAssetAtPath<SubtitleOptionsReference>(AssetDatabase.GUIDToAssetPath(AssetDatabase
                        .FindAssets("t:SubtitleOptionsReference", null)
                        .FirstOrDefault()));

            }

        }
#endif

    }

}
