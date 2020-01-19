using System.Collections;
using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/SubtitleController")]
    [RequireComponent(typeof(SubtitleRenderer))]
    public class SubtitleController : MonoBehaviour
    {

        private const float DEFAULT_VOLUME_SCALE = 1f;

#pragma warning disable CS0649
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        [TextArea(1, 10)]
        private string _subtitleText = "Hello, world.";

        [SerializeField]
        private float _durationPerLine = 1;

        [SerializeField]
        private Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        private Subtitle.Type _type;

        [SerializeField]
        private SubtitleOptionsReference _subtitleOptions;
#pragma warning restore CS0649

        public Subtitle.Position position => _position;

        public SubtitleOptionsReference subtitleOptions => _subtitleOptions;

        private SubtitleRenderer _subtitleRenderer;

        private Coroutine _loopThroughSubtitleLinesCoroutine;

        private void Awake()
        {

            _subtitleRenderer = gameObject.GetComponent<SubtitleRenderer>();

        }

        public void PlayOneShot(AudioClip clip, float volumeScale = DEFAULT_VOLUME_SCALE)
        {

            Stop();

            _audioSource.PlayOneShot(clip, volumeScale);

            _loopThroughSubtitleLinesCoroutine = StartCoroutine(LoopThroughSubtitleLines());

        }

        public void PlayOneShot()
        {

            Stop();

            _audioSource.PlayOneShot(_audioSource.clip, DEFAULT_VOLUME_SCALE);

            _loopThroughSubtitleLinesCoroutine = StartCoroutine(LoopThroughSubtitleLines());

        }

        public void Stop()
        {

            if (_loopThroughSubtitleLinesCoroutine != null)
            {

                StopCoroutine(_loopThroughSubtitleLinesCoroutine);

                _loopThroughSubtitleLinesCoroutine = null;

            }

            _audioSource.Stop();

        }

        private IEnumerator LoopThroughSubtitleLines()
        {

            var lines = _subtitleText.Trim().Split('\n');

            var duration = new WaitForSecondsRealtime(_durationPerLine);

            _subtitleRenderer.Show();

            foreach (var line in lines)
            {

                _subtitleRenderer.SetText(line);

                yield return duration;

            }

            _subtitleRenderer.Hide();

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
