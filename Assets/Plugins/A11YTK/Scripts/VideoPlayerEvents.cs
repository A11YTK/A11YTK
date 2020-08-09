using UnityEngine;
using UnityEngine.Video;

namespace A11YTK
{

    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlayerEvents : SourceEvents
    {

        private VideoPlayer _videoPlayer;

        private void Update()
        {

            if (_videoPlayer.isPlaying && _currentState.Equals(STATE.STOPPED))
            {

                _currentState = STATE.PLAYING;

                Started?.Invoke();

            }
            else if (_videoPlayer.isPlaying && _currentState.Equals(STATE.PLAYING))
            {

                Tick?.Invoke((float)_videoPlayer.time);

            }
            else if (!_videoPlayer.isPlaying && _currentState.Equals(STATE.PLAYING))
            {

                if (_videoPlayer.time > 0)
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

            _videoPlayer = gameObject.GetComponent<VideoPlayer>();

        }

    }

}
