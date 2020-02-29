using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Audio Source Controller")]
    [RequireComponent(typeof(SubtitleRenderer))]
    public class SubtitleAudioSourceController : SubtitleController
    {

#pragma warning disable CS0649
        [SerializeField]
        private AudioSource _audioSource;
#pragma warning restore CS0649

        protected override double _elapsedTime => _audioSource.time;

        protected override bool _isPlaying =>
            _audioSource && _audioSource.isPlaying && _audioSource.time < _audioSource.clip.length;

        private void OnValidate()
        {

            if (_audioSource == null)
            {

                _audioSource = gameObject.GetComponent<AudioSource>();

            }

        }

    }

}
