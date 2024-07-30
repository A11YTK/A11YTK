using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Audio Source Controller")]
    public class SubtitleAudioSourceController : SubtitleSourceController
    {

#pragma warning disable CS0649
        [SerializeField]
        private AudioSource _audioSource;
#pragma warning restore CS0649

        protected override double _elapsedTime => _audioSource.time;

        protected override bool _isPlaying =>
            _audioSource && _audioSource.clip && _audioSource.isPlaying && _audioSource.time < _audioSource.clip.length;

#if UNITY_EDITOR
        protected override void OnValidate()
        {

            base.OnValidate();

            if (_audioSource == null)
            {

                _audioSource = gameObject.GetComponent<AudioSource>();

            }

        }
#endif

    }

}
