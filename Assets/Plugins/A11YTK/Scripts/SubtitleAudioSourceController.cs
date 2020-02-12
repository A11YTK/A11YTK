using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle AudioSource Controller")]
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

        public override void Play()
        {

            base.Play();

            if (_audioSource)
            {

                _audioSource.Play();

            }

        }

        public override void Stop()
        {

            base.Stop();

            if (_audioSource)
            {

                _audioSource.Stop();

            }

        }

        private void OnValidate()
        {

            if (_audioSource == null)
            {

                _audioSource = gameObject.GetComponent<AudioSource>();

            }

        }

    }

}
