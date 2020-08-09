using UnityEngine;

namespace A11YTK
{

    public class SourceEvents : MonoBehaviour
    {

        internal enum STATE
        {

            STOPPED,

            PLAYING,

            PAUSED

        }

        public delegate void SourceEvent();

        public delegate void SourceEventWithTime(float time);

        public SourceEvent Started;

        public SourceEventWithTime Tick;

        public SourceEvent Paused;

        public SourceEvent Stopped;

        internal STATE _currentState = STATE.STOPPED;

    }

}
