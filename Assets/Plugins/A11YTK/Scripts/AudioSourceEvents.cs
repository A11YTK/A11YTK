using UnityEngine;

namespace A11YTK
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceEvents : SourceEvents
    {

        private AudioSource _audioSource;

        private void Update()
        {

            if (_audioSource.isPlaying && _currentState.Equals(STATE.STOPPED))
            {

                _currentState = STATE.PLAYING;

                Started?.Invoke();

            }
            else if (_audioSource.isPlaying && _currentState.Equals(STATE.PLAYING))
            {

                Tick?.Invoke(_audioSource.time);

            }
            else if (!_audioSource.isPlaying && _currentState.Equals(STATE.PLAYING))
            {

                if (_audioSource.time > 0)
                {

                    _currentState = STATE.PAUSED;

                    Paused?.Invoke();

                }
                else
                {

                    _currentState = STATE.STOPPED;

                    Stopped?.Invoke();

                }

            }

        }

        private void OnEnable()
        {

            _audioSource = gameObject.GetComponent<AudioSource>();

        }

    }

}
